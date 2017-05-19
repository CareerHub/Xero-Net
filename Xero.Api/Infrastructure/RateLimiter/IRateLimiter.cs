using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xero.Api.Infrastructure.RateLimiter {
    public interface IRateLimiter {
        Task WaitUntilLimit();
        bool CheckLimit();
    }
}
