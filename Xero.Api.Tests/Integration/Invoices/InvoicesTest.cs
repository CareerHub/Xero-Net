using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xero.Api.Core.Model;
using Xero.Api.Core.Model.Status;
using Xero.Api.Core.Model.Types;

namespace Xero.Api.Tests.Integration.Invoices {
    public abstract class InvoicesTest : ApiWrapperTest {
        public Task<Invoice> Given_an_draft_invoice(InvoiceType type = InvoiceType.AccountsPayable) {
            return Given_an_invoice(type);
        }

        public Task<Invoice> Given_an_authorised_invoice(InvoiceType type = InvoiceType.AccountsPayable) {
            return Given_an_invoice(type, InvoiceStatus.Authorised);
        }

        public Task<Invoice> Given_an_invoice(InvoiceType type = InvoiceType.AccountsPayable, InvoiceStatus status = InvoiceStatus.Draft, string invoiceNumber = null) {
            return Api.CreateAsync(new Invoice {
                Contact = new Contact { Name = "ABC Bank" },
                Type = type,
                Date = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddDays(90),
                LineAmountTypes = LineAmountType.Inclusive,
                Status = status,
                Number = invoiceNumber,
                LineItems = new List<LineItem>
                {
                    new LineItem
                    {
                        AccountCode = "200",
                        Description = "Good value item",
                        LineAmount = 100m
                    }
                }
            });
        }

        public Task<Invoice> Given_a_description_only_invoice(InvoiceType type = InvoiceType.AccountsPayable) {
            return Api.CreateAsync(new Invoice {
                Contact = new Contact { Name = "Richard" },
                Type = type,
                LineItems = new List<LineItem>
                {
                    new LineItem
                    {
                        Description = "This is description only",
                        LineAmount = 100m
                    }
                }
            });
        }
    }
}
