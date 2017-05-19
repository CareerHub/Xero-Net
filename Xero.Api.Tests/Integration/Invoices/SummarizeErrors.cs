using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xero.Api.Core.Model;
using Xero.Api.Core.Model.Status;
using Xero.Api.Core.Model.Types;
using Xero.Api.Infrastructure.Exceptions;

namespace Xero.Api.Tests.Integration.Invoices {
    [TestClass]
    public class SummarizeErrors : InvoicesTest {
        [TestMethod]
        public async Task summariseErrors_gives_200() {
            var invoices = await Given_a_bad_invoice(summariseErrors: false);

            Assert.IsTrue(invoices.Count(p => p.ValidationStatus == ValidationStatus.Error) == 1);
            Assert.IsTrue(invoices.Count(p => p.ValidationStatus == ValidationStatus.Ok) == 1);
        }

        [TestMethod]
        public void errors_gives_validation_exception() {
            Assert.ThrowsExceptionAsync<ValidationException>(() => Given_a_bad_invoice());
        }

        private Task<IEnumerable<Invoice>> Given_a_bad_invoice(InvoiceType type = InvoiceType.AccountsPayable, InvoiceStatus status = InvoiceStatus.Draft, bool summariseErrors = true) {
            Api.Invoices.SummarizeErrors(summariseErrors);

            return Api.CreateAsync(new[] {
                new Invoice
                {
                    Contact = new Contact {Name = "ABC Bank"},
                    Type = type,
                    Date = DateTime.UtcNow,
                    DueDate = DateTime.UtcNow.AddDays(90),
                    LineAmountTypes = LineAmountType.Inclusive,
                    Status = status,
                    LineItems = new List<LineItem>
                    {
                        new LineItem
                        {
                            AccountCode = "200",
                            Description = "Good value item",
                            LineAmount = 100m
                        }
                    }
                },
                new Invoice
                {
                    Contact = new Contact
                    {
                        Name = "ABC Bank",
                        EmailAddress = "this_is_!_valid"
                    },
                    Type = type,
                    Date = DateTime.UtcNow,
                    DueDate = DateTime.UtcNow.AddDays(90),
                    LineAmountTypes = LineAmountType.Inclusive,
                    Status = status,
                    LineItems = new List<LineItem>
                    {
                        new LineItem
                        {
                            AccountCode = "200",
                            Description = "Good value item",
                            LineAmount = 100m
                        }
                    }
                }
            });
        }
    }
}
