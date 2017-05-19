using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xero.Api.Core.Model;
using Xero.Api.Core.Model.Types;

namespace Xero.Api.Tests.Integration.Accounts {
    [TestClass]
    public class Find : ApiWrapperTest {
        [TestMethod]
        public async Task find_by_value() {
            var accounts = await Api.Accounts.Where("Type == \"OVERHEADS\"").FindAsync();

            var type = accounts.ToList().First().Type;

            Assert.AreEqual(AccountType.Overheads, type);
        }

        [TestMethod]
        public async Task find_by_id() {
            var filtered = await Api.Accounts.Where("Type == \"REVENUE\"").FindAsync();
            var expected = filtered.First();

            var account = await Api.Accounts.FindAsync(expected.Id);
            Assert.AreEqual(expected.Id, account.Id);
        }

        [TestMethod]
        public async Task finding_a_non_system_account_has_null_SystemAccount() {
            var newNonSystemAccount = await Api.CreateAsync(new Account {
                Code = Random.GetRandomString(10),
                Type = AccountType.OtherIncome,
                Description = "Consultation " + Random.GetRandomString(10),
                Name = "Consultation " + Random.GetRandomString(10)
            });

            var account = await Api.Accounts.FindAsync(newNonSystemAccount.Id);

            Assert.AreEqual(null, account.SystemAccount);
        }

        [TestMethod]
        public async Task find_accounts_ifmodifiedsince() {
            var newNonSystemAccount = await Api.CreateAsync(new Account {
                Code = Random.GetRandomString(10),
                Type = AccountType.OtherIncome,
                Description = "Consultation " + Random.GetRandomString(10),
                Name = "Consultation " + Random.GetRandomString(10)
            });

            var accounts = await Api.Accounts
                .ModifiedSince(DateTime.Now.AddMinutes(-1))
                .FindAsync();

            Assert.IsTrue(accounts.Any());
        }
    }
}
