using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xero.Api.Core.Model.Types;

namespace Xero.Api.Tests.Integration.Prepayments {
    [TestClass]
    public class Find : ApiWrapperTest {
        [TestMethod]
        public async Task find_all() {
            var prepayments = await Api.Prepayments.FindAsync();
            Assert.IsTrue(prepayments.Any());
        }

        [TestMethod]
        public async Task find_all_receive_prepayments() {
            var prepayments = await Api.Prepayments.Where("Type == \"RECEIVE-PREPAYMENT\"").FindAsync();
            Assert.IsTrue(prepayments.All(p => p.Type == PrepaymentType.ReceivePrepayment));
        }

        [TestMethod]
        public async Task find_all_spend_prepayments() {
            var prepayments = await Api.Prepayments.Where("Type == \"SPEND-PREPAYMENT\"").FindAsync();
            Assert.IsTrue(prepayments.All(p => p.Type == PrepaymentType.SpendPrepayment));
        }
    }
}
