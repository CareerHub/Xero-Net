using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xero.Api.Core.Model;
using Xero.Api.Core.Model.Status;
using Xero.Api.Core.Model.Types;

namespace Xero.Api.Tests.Integration.ExpenseClaims {
    public abstract class ExpenseClaimTest : ApiWrapperTest {
        public Task<Receipt> Given_a_receipt(Guid userId, string contactName, string description, decimal amount, string account) {
            return Api.CreateAsync(new Receipt {
                Date = DateTime.UtcNow.Date,
                Contact = new Contact { Name = contactName },
                LineAmountTypes = LineAmountType.Inclusive,
                Status = ReceiptStatus.Draft,
                LineItems = new List<LineItem>
                {
                    new LineItem
                    {
                        Description = description,
                        UnitAmount = amount,
                        AccountCode = account
                    }
                },
                Total = amount,
                User = new User {
                    Id = userId
                }
            });
        }

        public Task<ExpenseClaim> Given_an_expense_claim(Guid userId, Guid receiptId1, Guid receiptId2) {
            return Api.CreateAsync(new ExpenseClaim {
                User = new User {
                    Id = userId
                },
                Receipts = new List<Receipt>
                {
                    new Receipt
                    {
                        Id = receiptId1
                    },
                    new Receipt
                    {
                        Id = receiptId2
                    },
                }
            });
        }
    }
}
