using System;
using System.Threading.Tasks;
using Xero.Api.Common;
using Xero.Api.Core.File;
using Xero.Api.Core.Model.Types;
using Xero.Api.Infrastructure.Http;

namespace Xero.Api.Core.Endpoints {
    public class PdfEndpoint {
        private XeroHttpClient Client { get; set; }

        public PdfEndpoint(XeroHttpClient client) {
            Client = client;
        }

        public Task<BinaryFile> GetAsync(PdfEndpointType type, Guid parent) {
            var filename = parent.ToString("D") + ".pdf";
            var endpoint = $"/api.xro/2.0/{type}/{parent:D}";
            return Client.GetFileAsync(endpoint, MimeTypes.ApplicationPdf, filename);
        }
    }
}
