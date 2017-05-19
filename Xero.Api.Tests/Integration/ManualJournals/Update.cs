using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xero.Api.Tests.Integration.ManualJournals {
    [TestClass]
    public class Update : ManualJournalsTest {
        [TestInitialize]
        public Task UpdateSet() {
            return ManualJournalsSetUp();
        }

        [TestMethod]
        public async Task create_manual_journal() {
            const string expected = "We got that wrong";

            var manual = await Given_a_manual_journal("We know what we want to do", 50);

            manual.Narration = expected;

            var updated = await Api.UpdateAsync(manual);

            Assert.AreEqual(DateTime.Now.Date, updated.Date);
            Assert.AreEqual(expected, updated.Narration);
        }
    };
}