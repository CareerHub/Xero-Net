using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xero.Api.Core.Model.Types;
using Xero.Api.Infrastructure.Interfaces;
using Xero.Api.Serialization;

namespace Xero.Api.Tests.Unit
{
    [TestClass]
    public class SalesTaxBasisTypeTest
    {
        private IJsonObjectMapper _mapper;

        [TestInitialize]
        public void SetUp()
        {
            _mapper = new DefaultMapper();
        }

        [TestMethod]
        public void is_case_insensitive()
        {
            const SalesTaxBasisType expected = SalesTaxBasisType.Accural;

            Assert.IsTrue(expected == _mapper.From<SalesTaxBasisType>("accural"));
            Assert.IsTrue(expected == _mapper.From<SalesTaxBasisType>("AccuRAL"));
            Assert.IsTrue(expected == _mapper.From<SalesTaxBasisType>("ACCURAL"));
        }

        [TestMethod]
        public void accrual_options()
        {
            const SalesTaxBasisType expected = SalesTaxBasisType.Accural;

            Assert.IsTrue(expected == _mapper.From<SalesTaxBasisType>("ACCURAL"));
            Assert.IsTrue(expected == _mapper.From<SalesTaxBasisType>("ACCURALS"));
        }

        [TestMethod]
        public void cash_options()
        {
            Test(SalesTaxBasisType.Cash, "CASH");
        }

        [TestMethod]
        public void flat_rate_accrual_options()
        {
            Test(SalesTaxBasisType.FlatRateAccrual, "FlatRateAccrual");
        }

        [TestMethod]
        public void flat_rate_cash_options()
        {
            Test(SalesTaxBasisType.FlatRateCash, "FlatRateCash");
        }

        [TestMethod]
        public void invoice_options()
        {
            Test(SalesTaxBasisType.Invoice, "Invoice");
        }

        [TestMethod]
        public void none_options()
        {
            Test(SalesTaxBasisType.None, "NONE");
        }

        [TestMethod]
        public void payments_options()
        {
            Test(SalesTaxBasisType.Payments, "Payments");
        }

        private void Test(SalesTaxBasisType type, string value)
        {
            Assert.IsTrue(type == _mapper.From<SalesTaxBasisType>(value));
        }
    }
}