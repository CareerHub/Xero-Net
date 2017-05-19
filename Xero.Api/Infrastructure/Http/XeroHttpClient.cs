using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Extensions;
using Newtonsoft.Json.Linq;
using Xero.Api.Common;
using Xero.Api.Core.File;
using Xero.Api.Infrastructure.Exceptions;
using Xero.Api.Infrastructure.Interfaces;
using Xero.Api.Infrastructure.Model;
using Xero.Api.Infrastructure.RateLimiter;

namespace Xero.Api.Infrastructure.Http {
    // This makes the calls to the web as text and asks the object mappers to convert to and from objects to text.
    // This knows nothing about the types being passed to and fro. (Except for the constraint in the generic type)
    public class XeroHttpClient {
        private static readonly TimeSpan defaultTimeout = TimeSpan.FromMinutes(5.5);

        private readonly IJsonObjectMapper jsonMapper;
        private readonly IXmlObjectMapper xmlMapper;
        private readonly Uri baseUri;

        private readonly IAuthenticator auth;
        private readonly IConsumer consumer;
        private readonly IUser user;
        private readonly IRateLimiter rateLimiter;

        public DateTime? ModifiedSince { get; set; }
        public string Where { get; set; }
        public string Order { get; set; }
        public NameValueCollection Parameters { get; set; }
        public ProductInfoHeaderValue UserAgent { get; set; }

        public XeroHttpClient(string baseUri, IAuthenticator auth, IConsumer consumer, IUser user, IJsonObjectMapper jsonMapper, IXmlObjectMapper xmlMapper, IRateLimiter rateLimiter = null) {
            this.baseUri = new Uri(baseUri);
            this.auth = auth;
            this.consumer = consumer;
            this.user = user;
            this.rateLimiter = rateLimiter;
            this.jsonMapper = jsonMapper;
            this.xmlMapper = xmlMapper;
        }

        public async Task<IEnumerable<TResult>> GetAsync<TResult, TResponse>(string endPoint) where TResponse : IXeroResponse<TResult>, new() {
            var uri = GetUri(endPoint);
            using(var client = GetClient(HttpMethod.Get, uri)) {
                if(rateLimiter != null) await rateLimiter.WaitUntilLimit();
                var response = await client.GetAsync(uri);
                return await Read<TResult, TResponse>(response);
            }
        }
        
        internal async Task<IEnumerable<TResult>> PostAsync<TResult, TResponse>(string endPoint, byte[] data, string mimeType = MimeTypes.ApplicationXml) where TResponse : IXeroResponse<TResult>, new() {
            var uri = GetUri(endPoint);
            using(var client = GetClient(HttpMethod.Post, uri)) {
                var content = new ByteArrayContent(data);
                content.Headers.ContentType = new MediaTypeHeaderValue(mimeType);

                if(rateLimiter != null) await rateLimiter.WaitUntilLimit();
                var response = await client.PostAsync(uri, content);
                return await Read<TResult, TResponse>(response);
            }
        }

        public async Task<IEnumerable<TResult>> PostAsync<TResult, TResponse>(string endPoint, object data)
            where TResponse : IXeroResponse<TResult>, new() {
            var uri = GetUri(endPoint);
            using(var client = GetClient(HttpMethod.Post, uri)) {
                var content = new StringContent(xmlMapper.To(data), Encoding.UTF8, MimeTypes.ApplicationXml);

                if(rateLimiter != null) await rateLimiter.WaitUntilLimit();
                var response = await client.PostAsync(uri, content);
                return await Read<TResult, TResponse>(response);
            }
        }

        public async Task<IEnumerable<TResult>> PutAsync<TResult, TResponse>(string endPoint, object data)
            where TResponse : IXeroResponse<TResult>, new() {
            var uri = GetUri(endPoint);
            using(var client = GetClient(HttpMethod.Put, uri)) {
                var content = new StringContent(xmlMapper.To(data), Encoding.UTF8, MimeTypes.ApplicationXml);

                if(rateLimiter != null) await rateLimiter.WaitUntilLimit();
                var response = await client.PutAsync(uri, content);
                return await Read<TResult, TResponse>(response);
            }
        }

        public async Task<IEnumerable<TResult>> DeleteAsync<TResult, TResponse>(string endPoint)
            where TResponse : IXeroResponse<TResult>, new() {
            var uri = GetUri(endPoint);
            using(var client = GetClient(HttpMethod.Delete, uri)) {
                if(rateLimiter != null) await rateLimiter.WaitUntilLimit();
                var response = await client.DeleteAsync(uri);
                return await Read<TResult, TResponse>(response);
            }
        }

        private async Task<IEnumerable<TResult>> Read<TResult, TResponse>(HttpResponseMessage response)
            where TResponse : IXeroResponse<TResult>, new() {
            // this is the 'happy path'
            if(response.StatusCode == HttpStatusCode.OK) {
                var body = await response.Content.ReadAsStringAsync();
                return jsonMapper.From<TResponse>(body).Values;
            }

            await HandleErrors(response);
            return null;
        }

        public async Task<BinaryFile> GetFileAsync(string endpoint, string mimeType, string fileName, string query = null) {
            var uri = new Uri(baseUri, endpoint);
            using(var client = GetClient(HttpMethod.Get, uri, mimeType)) {

                if(rateLimiter != null) await rateLimiter.WaitUntilLimit();
                var response = await client.GetAsync(uri);

                if(response.StatusCode == HttpStatusCode.OK) {
                    var content = response.Content;
                    var stream = await content.ReadAsStreamAsync();
                    return new BinaryFile(stream, fileName, content.Headers.ContentType.MediaType, (int)content.Headers.ContentLength.Value);
                }

                var body = await response.Content.ReadAsStringAsync();
                await HandleErrors(response);
                return null;
            }
        }

        private Uri GetUri(string endpoint) {
            var qb = new QueryBuilder();

            if(!string.IsNullOrWhiteSpace(Where)) {
                qb.Add("where", Where);
            }

            if(!string.IsNullOrWhiteSpace(Order)) {
                qb.Add("order", Order);
            }

            if(Parameters != null) {
                foreach(string key in Parameters) {
                    qb.Add(key, Parameters[key]);
                }
            }

            return new Uri (baseUri, endpoint + qb.ToQueryString());

        }

        private HttpClient GetClient(HttpMethod method, Uri uri, string accept = MimeTypes.ApplicationJson) {
            var handler = new HttpClientHandler {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };

            var client = new HttpClient(handler);

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(accept));
            client.Timeout = defaultTimeout;

            if(ModifiedSince.HasValue) {
                client.DefaultRequestHeaders.IfModifiedSince = ModifiedSince.Value;
            }

            if(auth != null) {
                var oauthSignature = auth.GetSignature(consumer, user, uri, method.ToString(), consumer);
                client.DefaultRequestHeaders.Add("Authorization", oauthSignature);
            }

            client.DefaultRequestHeaders.UserAgent.Add(UserAgent ?? new ProductInfoHeaderValue("Xero-Api-wrapper-" + consumer.ConsumerKey, "1.0.0"));

            return client;
        }

        private async Task HandleErrors(HttpResponseMessage response) {
            var statusCode = response.StatusCode;
            var body = await response.Content.ReadAsStringAsync();

            if(statusCode == HttpStatusCode.BadRequest) {
                var data = jsonMapper.From<ApiException>(body);

                if(data.Elements != null && data.Elements.Any()) {
                    throw new ValidationException(data);
                }

                //Check for inline errors
                var jsonObject = JObject.Parse(body);
                var inlineValidationErrors = jsonObject.SelectTokens("$..ValidationErrors..Message").Select(p => new ValidationError { Message = p.ToString() }).ToList();

                if(inlineValidationErrors.Any()) {
                    data.Elements = new List<DataContractBase> { new DataContractBase { ValidationErrors = inlineValidationErrors } };
                    throw new ValidationException(data);
                }

                throw new BadRequestException(data);
            }

            if(statusCode == HttpStatusCode.Unauthorized) {
                throw new UnauthorizedException(body);
            }

            if(statusCode == HttpStatusCode.NotFound) {
                throw new NotFoundException(body);
            }

            if(statusCode == HttpStatusCode.InternalServerError) {
                throw new XeroApiException(statusCode, body);
            }

            if(statusCode == HttpStatusCode.ServiceUnavailable) {
                if(body.Contains("oauth_problem")) {
                    throw new RateExceededException(body);
                }

                throw new NotAvailableException(body);
            }

            if(statusCode == HttpStatusCode.NoContent) {
                return;
            }


            throw new XeroApiException(statusCode, body);
        }
    }
}
