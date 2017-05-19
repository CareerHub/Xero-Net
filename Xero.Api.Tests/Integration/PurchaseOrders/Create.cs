using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xero.Api.Core.Model;
using Xero.Api.Core.Model.Status;

namespace Xero.Api.Tests.Integration.PurchaseOrders {
    public class Create : ApiWrapperTest {
        [TestMethod]
        public async Task Create_minimal_draft_purchase_order() {
            var purchaseOrder = await Api.PurchaseOrders.CreateAsync(
                new PurchaseOrder {
                    Date = DateTime.Today,
                    Contact = new Contact { Id = (await Api.Contacts.FindAsync()).First().Id }
                }
            );

            Assert.IsTrue(purchaseOrder.Id != Guid.Empty);
            Assert.IsTrue(purchaseOrder.Status == PurchaseOrderStatus.Draft);
        }

        [TestMethod]
        public async Task Create_authorised_purchase_order() {
            var purchaseOrder = await Api.PurchaseOrders.CreateAsync(
                new PurchaseOrder {
                    Status = PurchaseOrderStatus.Authorised,
                    Date = DateTime.Today,
                    Contact = new Contact { Id = (await Api.Contacts.FindAsync()).First().Id },
                    LineItems = new List<LineItem>()
                    {
                        new LineItem
                        {
                            Description = "An item I want to purchase",
                            UnitAmount = 1,
                            Quantity = 1,

                        }
                    }
                }
            );

            Assert.IsTrue(purchaseOrder.Id != Guid.Empty);
            Assert.IsTrue(purchaseOrder.Status == PurchaseOrderStatus.Authorised);
        }

        public async Task<PurchaseOrder> Given_a_purchase_order() {
            return await Api.PurchaseOrders.CreateAsync(
                new PurchaseOrder {
                    Status = PurchaseOrderStatus.Authorised,
                    Date = DateTime.Today,
                    Contact = new Contact { Id = (await Api.Contacts.FindAsync()).First().Id },
                    LineItems = new List<LineItem>()
                    {
                        new LineItem
                        {
                            Description = "An item I want to purchase",
                            UnitAmount = 1,
                            Quantity = 1,

                        }
                    }
                }
            );
        }
    }
}
