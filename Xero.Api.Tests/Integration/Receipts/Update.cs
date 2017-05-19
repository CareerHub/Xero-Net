using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xero.Api.Core.Model.Status;

namespace Xero.Api.Tests.Integration.Receipts {
    [TestClass]
    public class Update : ReceiptTest {
        [TestMethod]
        public async Task delete_receipt() {
            var contact = Random.GetRandomString(10);
            var description = Random.GetRandomString(30);
            const ReceiptStatus expected = ReceiptStatus.Deleted;
            const decimal value = 13.8m;

            var receipt = await Given_a_receipt((await Api.Users.FindAsync()).First().Id, contact, description, value, "420");
            receipt.Status = expected;
            var deletedReceipt = await Api.UpdateAsync(receipt);

            Assert.AreEqual(expected, deletedReceipt.Status);
        }
    }
}