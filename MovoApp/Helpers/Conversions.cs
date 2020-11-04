using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace MovoApp.Helpers
{
    public static class Conversions
    {

        public static byte[] StringToByteArray(string hexString)
        {
            if (hexString.Length % 2 != 0)
            {
                throw new ArgumentException(String.Format(CultureInfo.InvariantCulture, "The binary key cannot have an odd number of digits: {0}", hexString));
            }

            byte[] data = new byte[hexString.Length / 2];
            for (int index = 0; index < data.Length; index++)
            {
                string byteValue = hexString.Substring(index * 2, 2);
                data[index] = byte.Parse(byteValue, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            }

            return data;
           /* return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();*/
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