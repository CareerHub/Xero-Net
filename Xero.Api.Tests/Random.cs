using System;
using System.Linq;

namespace Xero.Api.Tests {
    public static class Random {
        private const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private static System.Random random = new System.Random();

        public static string GetRandomString(int length) {
            var result = string.Empty;

            while(result.Length < length) {
                result = result + GetRandomString();
            }

            return result.ToUpper().Substring(0, length);
        }

        private static string GetRandomString() {
            return SanitiseBase64String(Convert.ToBase64String(Guid.NewGuid().ToByteArray()));
        }
        
        private static string SanitiseBase64String(string input) {
            return input
                .Replace("-", "")
                .Replace("=", "")
                .Replace("/", "")
                .Replace("+", "")
                .Replace(" ", "");
        }
    }
}
