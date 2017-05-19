using System;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace Xero.Api.Core.Model {
    public static class EnumExtensions {
        public static string GetEnumMemberValue(this Enum value) {
            var type = value.GetType();
            var memInfo = type.GetMember(value.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(EnumMemberAttribute), false);
            return (attributes.Any()) ? ((EnumMemberAttribute)attributes.First()).Value : value.ToString("");
        }

    }
}
