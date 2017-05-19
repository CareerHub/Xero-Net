using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xero.Api.Tests.Integration.Receipts {
    [TestClass]
    public class Create : ReceiptTest {
        [TestMethod]
        public async Task create_receipt() {
            var contact = Random.GetRandomString(10);
            var description = Random.GetRandomString(30);
            const decimal value = 13.8m;

            var receipt = await Given_a_receipt((await Api.Users.FindAsync()).First().Id, contact, description, value, "420");

            Assert.AreEqual(receipt.Total, value);
            Assert.AreEqual(receipt.Contact.Name, contact);
            Assert.AreEqual(receipt.LineItems[0].Description, description);
        }
    }
}

