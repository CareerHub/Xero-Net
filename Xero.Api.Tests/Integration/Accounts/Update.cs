using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xero.Api.Core.Model;
using Xero.Api.Core.Model.Status;
using Xero.Api.Core.Model.Types;

namespace Xero.Api.Tests.Integration.Accounts {
    public class Update : ApiWrapperTest {
        [TestMethod]
        public async Task Update_account() {
            var expectedDescription = "Updated Account" + Guid.NewGuid();

            var account = await CreateAccount();

            account.Description = expectedDescription;

            await Api.Accounts.UpdateAsync(account);

            var updated = await Api.Accounts.FindAsync(account.Id);

            Assert.IsTrue(updated.Description == expectedDescription);
        }


        [TestMethod]
        public async Task Archive_account() {
            var account = await CreateAccount();

            await Api.Accounts.UpdateAsync(new Account {
                Id = account.Id,
                Status = AccountStatus.Archived
            });

            var updated = await Api.Accounts.FindAsync(account.Id);

            Assert.IsTrue(updated.Status == AccountStatus.Archived);
        }

        private Task<Account> CreateAccount() {
            var code = "1234" + Guid.NewGuid();

            return Api.Accounts.CreateAsync(new Account {
                Code = code.Substring(0, 10),
                Name = "New Account " + Guid.NewGuid(),
                Type = AccountType.Sales
            });
        }

    }
}
