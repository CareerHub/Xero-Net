using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xero.Api.Core.Model;
using Xero.Api.Infrastructure.Exceptions;

namespace Xero.Api.Tests.Integration.General {
    [TestClass]
    public class Errors : ApiWrapperTest {
        [TestMethod]
        public void not_found() {
            Assert.ThrowsExceptionAsync<NotFoundException>(() => Api.Invoices.FindAsync("ThisIsNotThere"));
        }

        [TestMethod]
        public void bad_query() {
            Assert.ThrowsExceptionAsync<BadRequestException>(() => Api.Invoices.Where("Bob == Robert").FindAsync());
        }

        [TestMethod]
        public void validation_error() {
            Assert.ThrowsExceptionAsync<ValidationException>(() => Api.Invoices.CreateAsync(new Invoice {
                LineItems = new List<LineItem>
                {
                    new LineItem
                    {
                        LineAmount = 101.01M
                    }
                }
            }));
        }
    }
}
