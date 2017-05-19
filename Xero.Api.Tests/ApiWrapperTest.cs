using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Xero.Api.Core;
using Xero.Api.Core.Model;
using Xero.Api.Infrastructure.OAuth;
using Xero.Api.Serialization;

namespace Xero.Api.Tests {
    public class ApiWrapperTest {
        private static readonly DefaultMapper Mapper = new DefaultMapper();

        private static readonly XeroCoreApiConfig config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build()
                .GetSection("Api")
                .Get<XeroCoreApiConfig>();

        private Lazy<IXeroCoreApi> lazyApi = new Lazy<IXeroCoreApi>(() => new XeroCoreApi(
                config.BaseUrl,
                new PrivateAuthenticator(new X509Certificate2(config.SigningCertificatePath, config.SigningCertificatePassword, X509KeyStorageFlags.MachineKeySet)),
                new Consumer(config.ConsumerKey, config.ConsumerSecret),
                null,
                Mapper,
                Mapper,
                null
            ) {
                UserAgent = new ProductInfoHeaderValue("Xero-Api-Integration-tests", "1.0.0")
            });

        public IXeroCoreApi Api { get { return lazyApi.Value; } }

        protected Account BankAccount { get; set; }

        protected Account Account { get; set; }

        protected async Task SetUp() {
            BankAccount = (await Api.Accounts.Where("Type == \"BANK\"").FindAsync()).First();
            Account = (await Api.Accounts.Where("Type != \"BANK\"").FindAsync()).First();
        }
    }
}
