using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xero.Api.Core.Model.Status;
using Xero.Api.Core.Model.Types;

namespace Xero.Api.Tests.Integration.Invoices {
    [TestClass]
    public class OnlineInvoiceUrl : InvoicesTest {

        [TestMethod]
        public async Task find_the_online_invoice_url_for_an_accrec_invoice() {
            var invoice = await Given_an_invoice(InvoiceType.AccountsReceivable, InvoiceStatus.Authorised);

            var onlineInvoiceUrl = await Api.Invoices.RetrieveOnlineInvoiceUrlAsync(invoice.Id);

            Assert.IsTrue(!string.IsNullOrEmpty(onlineInvoiceUrl.OnlineInvoiceUrl));
        }
    }
}
