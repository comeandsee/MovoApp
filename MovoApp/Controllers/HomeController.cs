using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using MovoApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovoApp.Helpers;
using System.Security.Cryptography;
using Newtonsoft.Json;
using System.Text;

namespace MovoApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Comments(string token, string hmac)
        {
            ViewBag.Message = "Your comments page.";

            if (User.Identity.IsAuthenticated)
            {
                if(token !=null && hmac != null)
                {
                    var secretKey = "1884159e421e0f3470acd1b05a7471719976ec9bbc4bc0268ade51efb034d0f4";

                    var secretKeyBytes = Conversions.StringToByteArray(secretKey);
                    var tokenBytes = Conversions.StringToByteArray(token);
                    var hmacBytes = Conversions.StringToByteArray(hmac);
                    byte[] hashmessage = Conversions.HmacSha256(secretKeyBytes, tokenBytes);

                    if (hmacBytes.SequenceEqual(hashmessage))
                    {
                        ViewBag.Message = "Congratulation.";


                        User.Identity.GetUserId();

                        ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext()
                            .GetUserManager<ApplicationUserManager>()
                            .FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
                        var u = user.Email;
                        var userName = user.UserName;

                        PayloadMsg payloadMsg = new PayloadMsg(token, user.Email, user.UserName);

                        var jsonPayloadMsg = Newtonsoft.Json.JsonConvert.SerializeObject(payloadMsg);
                        // var jsonPayloadMsgBytes = Conversions.StringToByteArray(jsonPayloadMsg);
                        byte[] jsonPayloadMsgBytes = Encoding.UTF8.GetBytes(jsonPayloadMsg);

                        byte[] hmacMsg= Conversions.HmacSha256(secretKeyBytes, jsonPayloadMsg);
                        string hmacHex = Conversions.ByteArrayToString(hmacMsg);

                       

                        string payloadHex = Conversions.ByteArrayToString(jsonPayloadMsgBytes);


                        string redirectURL = "https://commento.io/api/oauth/sso/callback?payload=" + payloadHex + "&hmac=" + hmacHex;
                        return Redirect(redirectURL);

                    }


                }
            }


            

            return View();
        }

       

    }
}