using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xero.Api.Tests {
    public static class AssertExtensions {
        public delegate Task AsyncTestDelegate();

        public static void DoesNotThrowAsync(AsyncTestDelegate action) {
            action.Invoke().Wait();
        }
    }
}
