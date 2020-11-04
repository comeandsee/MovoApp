using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace MovoApp.Helpers
{
    public static class SSOConversions
    {

        public static byte[] StringToByteArray(string hexString)
        {
            if (hexString.Length % 2 != 0)
            {
                throw new ArgumentException(String.Format(CultureInfo.InvariantCulture, "The binary key cannot have an odd number of digits: {0}", hexString));
            };

            return Enumerable.Range(0, hexString.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hexString.Substring(x, 2), 16))
                             .ToArray();
        }

        public static string ByteArrayToString(byte[] ba)
        {
            return BitConverter.ToString(ba).Replace("-", "");
        }

        public static byte[] HmacSha256(byte[] secretKeyBytes, string tokenBytes)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(tokenBytes);
            return HmacSha256(secretKeyBytes, byteArray);

        }
        public static byte[] HmacSha256(byte[] secretKeyBytes, byte[] tokenBytes)
        {
            var hmacsha256 = new HMACSHA256(secretKeyBytes);
            byte[] hashmessage = hmacsha256.ComputeHash(tokenBytes);
            return hashmessage;
        }
    }
}