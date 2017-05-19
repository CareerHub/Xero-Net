using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Microsoft.Extensions.Primitives;

namespace Xero.Api.Common {
    public static class Extensions {
        public static void Add(this NameValueCollection collection, string name, Guid? value) {
            if(value.HasValue) {
                collection.Add(name, value.Value.ToString("D"));
            }
        }

        public static void Add(this NameValueCollection collection, string name, int? value) {
            if(value.HasValue) {
                collection.Add(name, value.Value.ToString("D"));
            }
        }

        public static void Add(this NameValueCollection collection, string name, bool? value) {
            if(value.HasValue) {
                collection.Add(name, value.Value.ToString().ToLower());
            }
        }

        public static string Get(this Dictionary<string, StringValues> collection, string name) {
            StringValues value;
            return collection.TryGetValue(name, out value) ? value.FirstOrDefault() : null;
        }
    }
}
