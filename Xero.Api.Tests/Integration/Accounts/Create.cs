using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xero.Api.Core.Model;
using Xero.Api.Core.Model.Types;
using Xero.Api.Infrastructure.Exceptions;

namespace Xero.Api.Tests.Integration.Accounts {
    [TestClass]
    public class Create : ApiWrapperTest {
        [TestMethod]
        public void create_account() {
            AssertExtensions.DoesNotThrowAsync(() => Api.CreateAsync(new Account {
                Code = Random.GetRandomString(10),
                Type = AccountType.Overheads,
                Description = "Consultant charges",
                Name = "Consultation " + Random.GetRandomString(10)
            }));

        }

        [TestMethod]
        public void create_bank_account() {
            AssertExtensions.DoesNotThrowAsync(() => Api.CreateAsync(new Account {
                Code = Random.GetRandomString(10),
                Name = "Cheque " + Random.GetRandomString(10),
                Type = AccountType.Bank,
                BankAccountNumber = "02-3467-474288",
            }));
        }

        [TestMethod]
        public void incorrect_types_are_rejected() {
            Assert.ThrowsExceptionAsync<ValidationException>(() => Api.CreateAsync(new Account {
                Code = Random.GetRandomString(10),
                Type = AccountType.Expense,
                Description = "Income from other sales",
                Name = "Other Sales",
                TaxType = "OUTPUT2"
            }));
        }
    }
}
