using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xero.Api.Tests.Integration.BankTransactions
{
    [TestClass]
    public class Create : BankTransactionTest
    {
        [TestMethod]
        public async Task create_bank_transactions()
        {
            var name = (await Given_a_bank_transaction())
                .Contact
                .Name;

            Assert.AreEqual("ABC Bank", name);
        }
    }
}
