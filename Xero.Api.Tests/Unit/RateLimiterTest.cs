using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xero.Api.Infrastructure.RateLimiter;

namespace Xero.Api.Tests.Unit {
    [TestClass]
    public class RateLimiterTest {
        [TestMethod]
        public async Task TestRateLimiter() {
            RateLimiter rate = new RateLimiter(TimeSpan.FromSeconds(5), 5);
            Stopwatch sw = new Stopwatch();
            sw.Start();
            Assert.IsFalse(rate.CheckLimit());
            await rate.WaitUntilLimit();
            Assert.IsFalse(rate.CheckLimit());
            await rate.WaitUntilLimit();
            Assert.IsFalse(rate.CheckLimit());
            await rate.WaitUntilLimit();
            Assert.IsFalse(rate.CheckLimit());
            await rate.WaitUntilLimit();
            Assert.IsFalse(rate.CheckLimit());
            await rate.WaitUntilLimit();
            Assert.IsTrue(rate.CheckLimit());
            await rate.WaitUntilLimit();
            sw.Stop();
            Assert.IsTrue(sw.Elapsed > TimeSpan.FromSeconds(5));
        }
    }
}
