using System;
using System.Security.Cryptography.X509Certificates;
using Xero.Api.Infrastructure.Interfaces;
using Xero.Api.Infrastructure.OAuth;
using Xero.Api.Infrastructure.OAuth.Signing;

namespace Xero.Api.Tests {
    public class PrivateAuthenticator : IAuthenticator {
        public X509Certificate2 Certificate { get; }
        public IUser User { get; set; }

        public PrivateAuthenticator(X509Certificate2 certificate) {
            Certificate = certificate;
        }

        public IToken GetToken(IConsumer consumer, IUser user) {
            return null;
        }

        public string GetSignature(IConsumer consumer, IUser user, Uri uri, string verb, IConsumer consumer1) {
            var token = new Token {
                ConsumerKey = consumer.ConsumerKey,
                ConsumerSecret = consumer.ConsumerSecret,
                TokenKey = consumer.ConsumerKey
            };

            return new RsaSha1Signer().CreateSignature(Certificate, token, uri, verb);
        }
    }
}
