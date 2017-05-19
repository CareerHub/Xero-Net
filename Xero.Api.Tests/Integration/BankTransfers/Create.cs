using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xero.Api.Tests.Integration.BankTransfers {
    [TestClass]
    public class Create : BankTransfersTest {
        [TestMethod]
        public async Task create_bank_transfer() {
            const decimal expected = 10m;
            var accounts = await get_bankaccount_ids();

            var bankTransfer = await Given_a_bank_transfer(expected);

            Assert.AreEqual(expected, bankTransfer.Amount);
            Assert.AreEqual(accounts[0], bankTransfer.FromBankAccount.Id);
            Assert.AreEqual(accounts[1], bankTransfer.ToBankAccount.Id);
        }
    }
}
