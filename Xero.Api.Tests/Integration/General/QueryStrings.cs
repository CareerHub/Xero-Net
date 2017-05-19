using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xero.Api.Tests.Integration.General {
    [TestClass]
    public class QueryStrings : ApiWrapperTest {
        [TestMethod]
        public void query_string_is_as_expected() {
            AssertExtensions.DoesNotThrowAsync(() => Api.Invoices.Where("Status == \"ACTIVE\"").FindAsync());
        }

        [TestMethod]
        public void complex_query_string_is_as_expected() {
            var startDate = DateTime.UtcNow.AddDays(-30).Date.ToString("yyyy-MM-dd");
            var endDate = DateTime.UtcNow.Date.ToString("yyyy-MM-dd");

            AssertExtensions.DoesNotThrowAsync(() => Api.Invoices.Where("Status == \"ACTIVE\"")
                .And(string.Format("DueDate >= DateTime.Parse(\"{0}\")", startDate))
                .And(string.Format("DueDate <= DateTime.Parse(\"{0}\")", endDate))
                .OrderByDescending("DueDate").FindAsync());
        }

        [TestMethod]
        public void contact_query_string_is_as_expected() {
            AssertExtensions.DoesNotThrowAsync(() => Api.Contacts.IncludeArchived(true).FindAsync());
        }
    }
}
