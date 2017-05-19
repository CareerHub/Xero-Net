using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xero.Api.Core.Model.Types;
using Xero.Api.Infrastructure.Interfaces;
using Xero.Api.Serialization;

namespace Xero.Api.Tests.Unit {
    [TestClass]
    public class SalesTaxPeriodTypeTest {
        private IJsonObjectMapper _mapper;

        [TestInitialize]
        public void SetUp() {
            _mapper = new DefaultMapper();
        }

        [TestMethod]
        public void is_case_insensitive() {
            const SalesTaxPeriodType expected = SalesTaxPeriodType.Quarterly;

            Assert.IsTrue(expected == _mapper.From<SalesTaxPeriodType>("QUARTERLY"));
            Assert.IsTrue(expected == _mapper.From<SalesTaxPeriodType>("QuarterLY"));
            Assert.IsTrue(expected == _mapper.From<SalesTaxPeriodType>("quarterly"));
        }

        [TestMethod]
        public void monthly_options() {
            const SalesTaxPeriodType expected = SalesTaxPeriodType.Monthly;

            Assert.IsTrue(expected == _mapper.From<SalesTaxPeriodType>("MONTHLY"));
            Assert.IsTrue(expected == _mapper.From<SalesTaxPeriodType>("ONEMONTHS"));
            Assert.IsTrue(expected == _mapper.From<SalesTaxPeriodType>("1MONTHLY"));
        }

        [TestMethod]
        public void two_monthly_options() {
            const SalesTaxPeriodType expected = SalesTaxPeriodType.TwoMonths;

            Assert.IsTrue(expected == _mapper.From<SalesTaxPeriodType>("2MONTHLY"));
            Assert.IsTrue(expected == _mapper.From<SalesTaxPeriodType>("TWOMONTHS"));
        }

        [TestMethod]
        public void annual_options() {
            const SalesTaxPeriodType expected = SalesTaxPeriodType.Annually;

            Assert.IsTrue(expected == _mapper.From<SalesTaxPeriodType>("YEARLY"));
            Assert.IsTrue(expected == _mapper.From<SalesTaxPeriodType>("ANNUALLY"));
        }

        [TestMethod]
        public void semi_annual_options() {
            const SalesTaxPeriodType expected = SalesTaxPeriodType.SixMonths;

            Assert.IsTrue(expected == _mapper.From<SalesTaxPeriodType>("6MONTHLY"));
            Assert.IsTrue(expected == _mapper.From<SalesTaxPeriodType>("SIXMONTHS"));
        }

        [TestMethod]
        public void quarterly_options() {
            const SalesTaxPeriodType expected = SalesTaxPeriodType.Quarterly;

            Assert.IsTrue(expected == _mapper.From<SalesTaxPeriodType>("3MONTHLY"));
            Assert.IsTrue(expected == _mapper.From<SalesTaxPeriodType>("QUARTERLY"));
            Assert.IsTrue(SalesTaxPeriodType.QuarterlyOne == _mapper.From<SalesTaxPeriodType>("QUARTERLY1"));
            Assert.IsTrue(SalesTaxPeriodType.QuarterlyTwo == _mapper.From<SalesTaxPeriodType>("QUARTERLY2"));
            Assert.IsTrue(SalesTaxPeriodType.QuarterlyThree == _mapper.From<SalesTaxPeriodType>("QUARTERLY3"));
        }
    }
}
