using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xero.Api.Core.Model;
using Xero.Api.Core.Model.Status;

namespace Xero.Api.Tests.Integration.PurchaseOrders {
    public class Update : ApiWrapperTest {
        [TestMethod]
        public async Task Can_update_a_PurchaseOrder_like_this() {
            var purchaseOrder = await Given_a_PurchaseOrder();

            purchaseOrder.LineItems.Add(new LineItem {
                Description = "Another item I want to purchase",
                UnitAmount = 10,
                Quantity = 10
            });

            var updatedPurchaseOrder = await Api.PurchaseOrders.UpdateAsync(purchaseOrder);

            Assert.AreEqual(2, updatedPurchaseOrder.LineItems.Count);
        }


        public async Task<PurchaseOrder> Given_a_PurchaseOrder() {
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
