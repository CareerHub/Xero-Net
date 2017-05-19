using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xero.Api.Tests.Integration.ExpenseClaims
{
    [TestClass]
    public class Create : ExpenseClaimTest
    {
        [TestMethod]
        public async Task create_expense_claim()
        {
            var user = (await Api.Users.FindAsync()).First();

            var receipt1 = await Given_a_receipt(user.Id, Random.GetRandomString(10), Random.GetRandomString(30), 20m, "420");
            var receipt2 = await Given_a_receipt(user.Id, Random.GetRandomString(10), Random.GetRandomString(30), 50m, "420");

            var claim = await Given_an_expense_claim(user.Id, receipt1.Id, receipt2.Id);

            Assert.AreEqual(70m, claim.Total);
        }
    }
}
