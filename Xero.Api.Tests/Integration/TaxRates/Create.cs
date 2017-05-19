using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xero.Api.Core.Model;
using Xero.Api.Core.Model.Types;

namespace Xero.Api.Tests.Integration.TaxRates {
    [TestClass]
    public class Create : TaxRateTest {
        [TestMethod]
        public async Task create_tax_rate() {
            var name = Random.GetRandomString(10);
            var state = Random.GetRandomString(5);
            var local = Random.GetRandomString(5);

            const decimal stateRate = 7.5m;
            const decimal localRate = 0.625m;
            const ReportTaxType taxType = ReportTaxType.Input;

            var taxRate = await Given_a_tax_rate(name, taxType, state, stateRate, local, localRate);

            Assert.IsTrue(name == taxRate.Name);
            Assert.IsTrue(taxType == taxRate.ReportTaxType);

            AssertContainsRate(taxRate, state, stateRate);
            AssertContainsRate(taxRate, local, localRate);
        }

        private void AssertContainsRate(TaxRate taxRate, string name, decimal rate) {
            var taxComponent = taxRate.TaxComponents.SingleOrDefault(p => name == p.Name);

            Assert.IsNotNull(taxComponent);
            Assert.AreEqual(taxComponent.Rate, rate);
        }
    }
}
