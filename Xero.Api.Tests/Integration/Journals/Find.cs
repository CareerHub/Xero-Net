using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xero.Api.Tests.Integration.Journals {
    [TestClass]
    public class Find : ApiWrapperTest {
        [TestMethod]
        public async Task find_journals() {
            var journals = await Api.Journals.FindAsync();

            Assert.IsTrue(journals.Any());
        }

        [TestMethod]
        public async Task find_journals_offset() {
            var journals = (await Api.Journals.FindAsync()).ToList();

            if(journals.Count() == 100) {
                var offset = journals.Max(p => p.Number);

                Assert.IsTrue((await Api.Journals.Offset(offset).FindAsync()).Any());
            }
        }
    }
}
