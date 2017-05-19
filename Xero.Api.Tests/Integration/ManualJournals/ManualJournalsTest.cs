using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xero.Api.Core.Model;
using Xero.Api.Core.Model.Types;

namespace Xero.Api.Tests.Integration.ManualJournals {
    public class ManualJournalsTest : ApiWrapperTest {
        protected Account Sales { get; private set; }
        protected Account Revenue { get; private set; }

        protected async Task ManualJournalsSetUp() {
            await SetUp();
            Sales = await Given_an_account();
            Revenue = await Given_an_account(AccountType.Revenue);
        }

        private async Task<Account> Given_an_account(AccountType type = AccountType.Sales) {
            var accounts = await Api.Accounts
                .Where(string.Format("Type == \"{0}\"", type.ToString().ToUpper()))
                .FindAsync();

            if(accounts.Any()) {
                return accounts.FirstOrDefault();
            }

            return await Api.CreateAsync(new Account {
                Name = Random.GetRandomString(20),
                Code = Random.GetRandomString(10),
                Type = type
            });
        }

        protected Task<ManualJournal> Given_a_manual_journal(string narration, decimal amount) {
            return Api.CreateAsync(new ManualJournal {
                Narration = narration,
                Lines = new List<Line>
                {
                    new Line
                    {
                        Amount = amount,
                        AccountCode = Sales.Code
                    },
                    new Line
                    {
                        Amount = -amount,
                        AccountCode = Revenue.Code
                    }
                }
            });
        }
    }
}