using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xero.Api.Tests.Integration.General {
    [TestClass]
    public class FindingAllItems : ApiWrapperTest {
        [TestMethod]
        public void get_accounts() {
            AssertExtensions.DoesNotThrowAsync(() => Api.Accounts.FindAsync());
        }

        [TestMethod]
        public void get_bank_transactions() {
            AssertExtensions.DoesNotThrowAsync(() => Api.BankTransactions.FindAsync());
        }

        [TestMethod]
        public void get_bank_transfers() {
            AssertExtensions.DoesNotThrowAsync(() => Api.BankTransfers.FindAsync());
        }

        [TestMethod]
        public void get_branding_themes() {
            AssertExtensions.DoesNotThrowAsync(() => Api.BrandingThemes.FindAsync());
        }

        [TestMethod]
        public void get_contacts() {
            AssertExtensions.DoesNotThrowAsync(() => Api.Contacts.FindAsync());
        }

        [TestMethod]
        public void get_credit_notes() {
            AssertExtensions.DoesNotThrowAsync(() => Api.CreditNotes.FindAsync());
        }

        [TestMethod]
        public void get_currencies() {
            AssertExtensions.DoesNotThrowAsync(() => Api.Currencies.FindAsync());
        }

        [TestMethod]
        public void get_employees() {
            AssertExtensions.DoesNotThrowAsync(() => Api.Employees.FindAsync());
        }

        [TestMethod]
        public void get_expense_claims() {
            AssertExtensions.DoesNotThrowAsync(() => Api.ExpenseClaims.FindAsync());
        }

        [TestMethod]
        public void get_invoices() {
            AssertExtensions.DoesNotThrowAsync(() => Api.Invoices.FindAsync());
        }

        [TestMethod]
        public void get_journals() {
            AssertExtensions.DoesNotThrowAsync(() => Api.Journals.FindAsync());
        }

        [TestMethod]
        public void get_manual_journals() {
            AssertExtensions.DoesNotThrowAsync(() => Api.ManualJournals.FindAsync());
        }

        [TestMethod]
        public void get_payments() {
            AssertExtensions.DoesNotThrowAsync(() => Api.Payments.FindAsync());
        }

        [TestMethod]
        public void get_receipts() {
            AssertExtensions.DoesNotThrowAsync(() => Api.Receipts.FindAsync());
        }

        [TestMethod]
        public void get_repeating_invoices() {
            AssertExtensions.DoesNotThrowAsync(() => Api.RepeatingInvoices.FindAsync());
        }

        [TestMethod]
        public void get_tax_rates() {
            AssertExtensions.DoesNotThrowAsync(() => Api.TaxRates.FindAsync());
        }

        [TestMethod]
        public void get_users() {
            AssertExtensions.DoesNotThrowAsync(() => Api.Users.FindAsync());
        }
    }
}
