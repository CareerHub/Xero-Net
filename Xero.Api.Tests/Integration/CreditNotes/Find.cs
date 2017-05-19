using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xero.Api.Core.Model.Types;

namespace Xero.Api.Tests.Integration.CreditNotes {
    [TestClass]
    public class Find : CreditNotesTest {
        [TestMethod]
        public async Task find_all_creditnotes() {
            await Given_a_creditnote();

            Assert.IsTrue((await Api.CreditNotes.FindAsync()).Any());
        }

        [TestMethod]
        public async Task find_by_id() {
            var expected = (await Given_a_creditnote()).Id;
            var id = (await Api.CreditNotes.FindAsync(expected)).Id;

            Assert.AreEqual(expected, id);
        }

        [TestMethod]
        public async Task find_by_value() {
            await Given_a_creditnote();

            var creditnote = (await Api.CreditNotes
                .Where("Type == \"ACCPAYCREDIT\"")
                .FindAsync())
                .First()
                .Type;

            Assert.AreEqual(CreditNoteType.AccountsPayable, creditnote);
        }

        [TestMethod]
        public async Task find_orderby_value() {
            await Given_a_creditnote();

            var creditNote = (await Api.CreditNotes
                .OrderBy("Type")
                .FindAsync())
                .First()
                .Type;

            Assert.AreEqual(CreditNoteType.AccountsPayable, creditNote);
        }
    }
}
