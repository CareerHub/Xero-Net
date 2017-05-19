using System.Net.Http.Headers;
using Xero.Api.Infrastructure.Http;
using Xero.Api.Infrastructure.Interfaces;
using Xero.Api.Infrastructure.RateLimiter;

namespace Xero.Api.Common {
    // It is used to plug together the the components which are used for authentication and serialization.
    public abstract class XeroApi {
        protected XeroHttpClient Client { get; }

        public string BaseUri { get; }

        public ProductInfoHeaderValue UserAgent {
            get { return Client.UserAgent; }
            set { Client.UserAgent = value; }
        }

        private XeroApi(string baseUri) {
            BaseUri = baseUri;
        }

        protected XeroApi(string baseUri, IAuthenticator auth, IConsumer consumer, IUser user, IJsonObjectMapper readMapper, IXmlObjectMapper writeMapper, IRateLimiter rateLimiter)
            : this(baseUri) {
            Client = new XeroHttpClient(baseUri, auth, consumer, user, readMapper, writeMapper, rateLimiter);
        }
    }
}