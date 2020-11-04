using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using MovoApp.Helpers;
using MovoApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MovoApp.Controllers
{
    public class CommentsController : Controller
    {
        // GET: Comments
        public ActionResult Index(string hmac, string token)
        {
            
            byte[] expectedHmac = SSOConversions.HmacSha256(SSOConstants.SecretKeyBytes, SSOConversions.StringToByteArray(token));
            byte[] hmacBytes = SSOConversions.StringToByteArray(hmac);
     
            if (hmacBytes.SequenceEqual(expectedHmac))
            {
                //create payload in json
                ApplicationUser user = GetCurrentUser();
                PayloadAuth payloadAuth = new PayloadAuth(token, user.Email, User.Identity.GetUserName());
                var jsonPayloadMsg = Newtonsoft.Json.JsonConvert.SerializeObject(payloadAuth);


                //covert hmac & payload to HEX
                byte[] hmacMsg = SSOConversions.HmacSha256(SSOConstants.SecretKeyBytes, jsonPayloadMsg);
                string hmacHex = SSOConversions.ByteArrayToString(hmacMsg);
                byte[] jsonPayloadMsgBytes = Encoding.UTF8.GetBytes(jsonPayloadMsg);
                string payloadHex = SSOConversions.ByteArrayToString(jsonPayloadMsgBytes);

                //redirect
                return Redirect(SSOConstants.RedirectURL(payloadHex, hmacHex));

            }

           return RedirectToAction("Comments", "Home", new { validValues = false });
        }

        private static ApplicationUser GetCurrentUser()
        {
            return System.Web.HttpContext.Current.GetOwinContext()
                .GetUserManager<ApplicationUserManager>()
                .FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
        }
    }
}