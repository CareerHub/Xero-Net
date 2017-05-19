using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xero.Api.Core.Model.Types;

namespace Xero.Api.Tests.Integration.Overpayments {
    [TestClass]
    public class Find : ApiWrapperTest {
        [TestMethod]
        public async Task find_all() {
            var overpayments = await Api.Overpayments.FindAsync();
            Assert.IsTrue(overpayments.Any());
        }

        [TestMethod]
        public async Task find_all_receive_overpayments() {
            var overpayments = await Api.Overpayments.Where("Type == \"RECEIVE-OVERPAYMENT\"").FindAsync();
            Assert.IsTrue(overpayments.All(p => p.Type == OverpaymentType.ReceiveOverpayment));
        }

        [TestMethod]
        public async Task find_all_spend_overpayments() {
            var overpayments = await Api.Overpayments.Where("Type == \"SPEND-OVERPAYMENT\"").FindAsync();
            Assert.IsTrue(overpayments.All(p => p.Type == OverpaymentType.SpendOverpayment));
        }
    }
}
