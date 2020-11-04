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

        public ActionResult Comments(string token, string hmac, bool validValues=true)
        {
            ViewBag.Message = " Your comments page. ";

            if (!validValues)
            {
                ViewBag.Message = "Token or hmac is not valid.";
            }
            else if (User.Identity.IsAuthenticated & token != null && hmac != null)
            {
                return RedirectToAction("Index", "Comments", new { token = token, hmac =hmac });
            }
 
            return View();
        }

       


    }
}