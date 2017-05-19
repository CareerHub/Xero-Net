using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xero.Api.Core.Model.Types;

namespace Xero.Api.Tests.Integration.CreditNotes {
    [TestClass]
    public class Create : CreditNotesTest {
        [TestMethod]
        public async Task create_creditnote() {
            const CreditNoteType expected = CreditNoteType.AccountsReceivable;

            var type = (await Given_a_creditnote(type: expected)).Type;

            Assert.AreEqual(expected, type);
        }
    }
}

