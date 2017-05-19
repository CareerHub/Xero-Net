using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xero.Api.Core.Model.Status;

namespace Xero.Api.Tests.Integration.Payments {
    [TestClass]
    public class Delete : PaymentsTest {
        [TestInitialize]
        public Task CreatePaymentsSetUp() {
            return SetUp();
        }

        [TestMethod]
        public async Task can_delete_payments() {
            var date = DateTime.UtcNow;
            const decimal expected = 32.6m;
            const decimal invoiceAmount = 100;

            var payment = await Given_a_payment(invoiceAmount, date, expected);

            await Given_this_payment_is_deleted(payment);

            var found = await Api.Payments.FindAsync(payment.Id);

            Assert.IsTrue(found.Status == PaymentStatus.Deleted);
        }

    }
}
