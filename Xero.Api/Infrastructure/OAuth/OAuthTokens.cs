using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;
using Xero.Api.Common;
using Xero.Api.Infrastructure.Interfaces;

namespace Xero.Api.Infrastructure.OAuth {
    public class OAuthTokens {
        private readonly string _authorizeUri;
        private readonly string _tokenUri;
        private const string XeroRequestUri = "oauth/RequestToken";
        private const string XeroAccessTokenUri = "oauth/AccessToken";
        private const string XeroAuthorizeUri = "oauth/Authorize";

        public OAuthTokens(string authorizeUri, string tokenUri) {
            _authorizeUri = authorizeUri;
            _tokenUri = tokenUri;
        }

        public string AuthorizeUri {
            get {
                var uri = new UriBuilder(_authorizeUri) {
                    Path = XeroAuthorizeUri
                };

                return uri.ToString();
            }
        }

        public string RequestUri {
            get {
                return XeroRequestUri;
            }
        }

        public string AccessUri {
            get {
                return XeroAccessTokenUri;
            }
        }

        public Task<IToken> GetRequestTokenAsync(IConsumer consumer, string header) {
            return GetTokenAsync(_tokenUri, new Token { ConsumerKey = consumer.ConsumerKey, ConsumerSecret = consumer.ConsumerSecret }, XeroRequestUri, header);
        }

        public Task<IToken> GetAccessTokenAsync(IToken token, string header) {
            return GetTokenAsync(_tokenUri, token, XeroAccessTokenUri, header);
        }

        public Task<IToken> RenewAccessTokenAsync(IToken token, string header) {
            return GetTokenAsync(_tokenUri, token, XeroAccessTokenUri, header);
        }

        public async Task<IToken> GetTokenAsync(string baseUri, IToken consumer, string endPoint, string header) {
            using(var client = new HttpClient()) {
                client.BaseAddress = new Uri(baseUri);
                client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("Xero Api wrapper - " + consumer.ConsumerKey));
                client.DefaultRequestHeaders.Add("Authorization", header);

                var content = new StringContent(string.Empty);
                var response = await client.PostAsync(endPoint, content);
                var body = await response.Content.ReadAsStringAsync();

                if(response.StatusCode != HttpStatusCode.OK) {
                    if(body.Contains("oauth_problem")) {
                        throw new OAuthException(body);
                    }

                    throw new UnexpectedOauthResponseException(response.StatusCode, body);
                }

                var qs = QueryHelpers.ParseQuery(body);
                var expires = qs.Get("oauth_expires_in");
                var session = qs.Get("oauth_session_handle");

                var token = new Token(consumer.ConsumerKey, consumer.ConsumerSecret) {
                    TokenKey = qs.Get("oauth_token"),
                    TokenSecret = qs.Get("oauth_token_secret"),
                    OrganisationId = qs.Get("xero_org_muid")
                };

                if(!string.IsNullOrWhiteSpace(expires)) {
                    token.ExpiresAt = DateTime.UtcNow.AddSeconds(int.Parse(expires));
                }

                if(!string.IsNullOrWhiteSpace(session)) {
                    token.Session = session;
                    token.SessionExpiresAt = DateTime.UtcNow.AddSeconds(int.Parse(qs.Get("oauth_authorization_expires_in")));
                }

                return token;
            }
        }
    }
}
