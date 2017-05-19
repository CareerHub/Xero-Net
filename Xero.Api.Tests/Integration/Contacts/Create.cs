using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xero.Api.Core.Model;
using Xero.Api.Core.Model.Status;

namespace Xero.Api.Tests.Integration.Contacts {
    [TestClass]
    public class Create : ContactsTest {
        [TestMethod]
        public async Task create_contact() {
            var name = (await Given_a_contact()).Name;

            Assert.IsTrue(name.StartsWith("Peter"));
        }

        [TestMethod]
        public async Task create_many_contact() {
            var contacts = await Api.CreateAsync(new List<Contact>
            {
                new Contact
                {
                    Name = "John " + Random.GetRandomString(10)
                },
                new Contact
                {
                    Name = "Paul" + Random.GetRandomString(10)
                },
                new Contact
                {
                    Name = "George" + Random.GetRandomString(10)
                },
                new Contact
                {
                    Name = "Ringo" + Random.GetRandomString(10)
                }
            });

            Assert.AreEqual(4, contacts.Count());
        }

        [TestMethod]
        public async Task create_complex_contact() {
            Api.Contacts.SummarizeErrors(true);

            var expectedAccountNumber = "AccountNumber" + Random.GetRandomString(10);

            var contact = await Api.CreateAsync(new Contact {
                Name = "24 locks " + Random.GetRandomString(10),
                FirstName = "Ben",
                LastName = "Bowden",
                EmailAddress = "ben.bowden@24locks.com",
                AccountNumber = expectedAccountNumber,

                ContactPersons = new List<ContactPerson>
                {
                    new ContactPerson
                    {
                        FirstName = "John",
                        LastName = "Smith",
                        EmailAddress = "john.smith@24locks.com",
                        IncludeInEmails = true
                    }
                }
            });

            Assert.AreEqual("Ben", contact.FirstName);
            Assert.AreEqual("John", contact.ContactPersons[0].FirstName);
            Assert.AreEqual(expectedAccountNumber, contact.AccountNumber);
        }
    }
}
