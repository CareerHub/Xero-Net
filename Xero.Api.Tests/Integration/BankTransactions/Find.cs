using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xero.Api.Tests.Integration.BankTransactions {
    [TestClass]
    public class Find : BankTransactionTest {
        [TestMethod]
        public void find_bank_transactions_where_filter() {
            AssertExtensions.DoesNotThrowAsync(() => Api.BankTransactions
                .Where("Type == \"SPEND\"")
                .And("Status == \"AUTHORISED\"")
                .FindAsync());
        }

        [TestMethod]
        public async Task find_bank_transactions_ifmodifiedsince() {
            await Given_a_bank_transaction();

            var bankTransaction = await Api.BankTransactions
                .ModifiedSince(DateTime.Today.AddDays(-1).Date)
                .FindAsync();

            Assert.IsNotNull(bankTransaction);
        }

        [TestMethod]
        public async Task find_bank_transactions_individual() {
            var expected = (await Given_a_bank_transaction()).Id;

            var id = (await Api.BankTransactions.FindAsync(expected)).Id;

            Assert.AreEqual(expected, id);
        }

        [TestMethod]
        public async Task find_by_page() {
            await Given_a_bank_transaction();
            var bankTrans = await Api.BankTransactions.Page(1).FindAsync();

            Assert.IsTrue(bankTrans.Any());
        }
    }
}
