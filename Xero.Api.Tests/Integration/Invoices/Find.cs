using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xero.Api.Core.Model.Types;

namespace Xero.Api.Tests.Integration.Invoices {
    [TestClass]
    public class Find : InvoicesTest {
        [TestMethod]
        public async Task find_by_page() {
            await Given_an_invoice();
            var invoices = await Api.Invoices.Page(1).FindAsync();

            Assert.IsTrue(invoices.Any());
        }

        [TestMethod]
        public async Task find_by_id() {
            var expected = (await Given_an_invoice()).Id;
            var id = (await Api.Invoices.FindAsync(expected)).Id;

            Assert.AreEqual(expected, id);
        }

        [TestMethod]
        public async Task find_by_id_list() {
            var created = await Given_an_invoice();
            var invoices = (await Api.Invoices.Ids(new[] { created.Id }).FindAsync()).ToList();

            Assert.AreEqual(1, invoices.Count());
            Assert.AreEqual(created.Id, invoices.First().Id);
        }

        [TestMethod]
        public async Task find_by_statuses_list() {
            var created = await Given_an_invoice();
            var invoices = (await Api.Invoices.Statuses(new[] { created.Status }).FindAsync()).ToList();

            Assert.IsTrue(invoices.Any(it => it.Id == created.Id));
        }

        [TestMethod]
        public async Task find_by_contact_id_list() {
            var created = await Given_an_invoice();
            var invoices = (await Api.Invoices.ContactIds(new[] { created.Contact.Id }).FindAsync()).ToList();

            Assert.IsTrue(invoices.Any(it => it.Id == created.Id));
        }

        [TestMethod]
        public async Task find_by_invoice_number_list() {
            var created = await Given_an_invoice(invoiceNumber: Guid.NewGuid().ToString());
            var invoices = (await Api.Invoices.InvoiceNumbers(new[] { created.Number }).FindAsync()).ToList();

            Assert.IsTrue(invoices.Any(it => it.Id == created.Id));
        }

        [TestMethod]
        public async Task find_by_mixture_of_query_param_lists() {
            var created = await Given_an_invoice(invoiceNumber: Guid.NewGuid().ToString());
            var invoices = (await Api.Invoices
                .Ids(new[] { created.Id })
                .ContactIds(new[] { created.Contact.Id })
                .Statuses(new[] { created.Status })
                .InvoiceNumbers(new[] { created.Number })
                .FindAsync()).ToList();

            Assert.IsTrue(invoices.Any(it => it.Id == created.Id));
        }

        [TestMethod]
        public async Task find_by_value() {
            await Given_an_invoice();
            var invoices = (await Api.Invoices
                .Where("Type == \"ACCREC\"")
                .FindAsync())
                .ToList();

            Assert.IsTrue(invoices.Any());
            Assert.IsTrue(invoices.All(p => p.Type == InvoiceType.AccountsReceivable));
        }

        [TestMethod]
        public async Task find_by_due_date() {
            await Given_an_invoice();

            var today = DateTime.UtcNow;

            var invoices = (await Api.Invoices
                .Where(string.Format("DueDate > DateTime({0},{1},{2})", today.Year, today.Month, today.Day))
                .FindAsync())
                .ToList();

            Assert.IsTrue(invoices.Any());
        }

        [TestMethod]
        public async Task order_by_type() {
            var invoices = await Api.Invoices.OrderByDescending("Type").FindAsync();

            Assert.AreEqual(InvoiceType.AccountsReceivable, invoices.First().Type);
        }
    }
}
