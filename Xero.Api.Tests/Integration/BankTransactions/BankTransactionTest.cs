using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xero.Api.Core.Model;
using Xero.Api.Core.Model.Types;

namespace Xero.Api.Tests.Integration.BankTransactions {
    public class BankTransactionTest : ApiWrapperTest {
        public Task<BankTransaction> Given_a_bank_transaction() {
            return Given_a_bank_transaction(BankTransactionType.Spend);
        }

        public async Task<BankTransaction> Given_a_bank_transaction(BankTransactionType type) {
            var id = await FindBankAccountGuid();

            return await Api.CreateAsync(new BankTransaction {
                Type = type,
                Contact = new Contact { Name = "ABC Bank" },
                LineItems = new List<LineItem>
                {
                    new LineItem
                    {
                        Description = "Yearly Bank Account Fee",
                        Quantity = 1m,
                        UnitAmount = 20.00m,
                        AccountCode = "404"
                    }
                },
                BankAccount = new Account { Id = id }
            });
        }

        public async Task<BankTransaction> Given_an_overpayment(BankTransactionType type) {
            var id = await FindBankAccountGuid();

            return await Api.CreateAsync(new BankTransaction {
                Type = type,
                Contact = new Contact { Name = "ABC Bank" },
                LineAmountTypes = LineAmountType.NoTax,
                LineItems = new List<LineItem>
                {
                    new LineItem
                    {
                        Description = "Yearly Bank Account Fee",
                        UnitAmount = 20.00m,
                        AccountCode = "800"
                    }
                },
                BankAccount = new Account { Id = id }
            });
        }

        public async Task<Guid> FindBankAccountGuid() {
            var bankAccount = (await Api.Accounts.Where("Type == \"BANK\"").FindAsync()).FirstOrDefault();

            if(bankAccount != null) {
                return bankAccount.Id;
            }

            return Guid.Empty;
        }
    }
}
