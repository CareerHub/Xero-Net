using System.Threading.Tasks;
using Xero.Api.Core.Model;

namespace Xero.Api.Tests.Integration.Contacts {
    public abstract class ContactsTest : ApiWrapperTest {

        protected Task<Contact> Given_a_contact() {
            return Api.CreateAsync(new Contact {
                Name = "Peter " + Random.GetRandomString(10)
            });
        }
    }
}
