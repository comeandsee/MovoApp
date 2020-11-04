using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovoApp.Helpers
{
    public static class SSOConstants
    {
        private static string secretKey = "1884159e421e0f3470acd1b05a7471719976ec9bbc4bc0268ade51efb034d0f4";
        
        public static string SecretKey { get => secretKey;  }
        public static byte[] SecretKeyBytes => SSOConversions.StringToByteArray(SecretKey);

        public static string RedirectURL(string payloadHex, string hmacHex)
        {
           return "https://commento.io/api/oauth/sso/callback?payload=" + payloadHex + "&hmac=" + hmacHex;
        }
      

    }
}