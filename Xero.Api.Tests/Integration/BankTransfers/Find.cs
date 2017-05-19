using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xero.Api.Tests.Integration.BankTransfers {
    [TestClass]
    public class Find : BankTransfersTest {
        [TestMethod]
        public async Task find_bank_transfers() {
            await Given_a_bank_transfer(10m);

            var allTransfers = await Api.BankTransfers.FindAsync();

            Assert.IsTrue(allTransfers.Any());
        }

        [TestMethod]
        public async Task find_bank_transfers_individual() {
            var expected = (await Given_a_bank_transfer(25m)).Id;

            var id = (await Api.BankTransfers.FindAsync(expected)).Id;

            Assert.AreEqual(expected, id);
        }

        [TestMethod]
        public async Task find_bank_transfers_ifmodifiedsince() {
            await Given_a_bank_transfer(25m);

            var date = DateTime.Today.AddDays(-4);
            var bankTransfers = await Api.BankTransfers.ModifiedSince(date).FindAsync();

            Assert.IsTrue(bankTransfers.Any());
        }
    }
}
