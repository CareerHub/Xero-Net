using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xero.Api.Core.Model.Types;

namespace Xero.Api.Tests.Integration.Organisation {
    [TestClass]
    public class Find : ApiWrapperTest {
        [TestMethod]
        public async Task can_get_the_organisation_sales_tax_basis() {
            var test = (await Api.GetOrganisationAsync()).SalesTaxBasisType;

            Assert.IsTrue(Enum.IsDefined(typeof(SalesTaxBasisType), test));
        }

        [TestMethod]
        public async Task can_get_the_organisation_sales_tax_period() {
            var test = (await Api.GetOrganisationAsync()).SalesTaxPeriod;

            Assert.IsTrue(Enum.IsDefined(typeof(SalesTaxPeriodType), test));
        }
    }
}
