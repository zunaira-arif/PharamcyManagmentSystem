using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PharmacyManagmentSystem.Models;
using PharmacyManagmentSystem.DAL;
namespace PharmacyManagmentSystem.Controllers
{
    public class AccountController : Controller
    {
        PharmacyDAL pdal = new PharmacyDAL();

        public ActionResult SignIn(string ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignIn(user model, string ReturnUrl)
        {
            ViewBag.ReturnUrl= pdal.Login(model.userName, model.password);
           
            // ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff(user model, string ReturnUrl)
        {
            pdal.SignOut();

            return RedirectToAction("Index", "Home");

        }
        //[Authorize (Roles ="CEO")]
        //public ActionResult something()
        //{
        //    return "";
        
        //}






	}
}

 //public ActionResult SignIn(string ReturnUrl)
 //       {
 //           ViewBag.ReturnUrl = ReturnUrl;
 //           return View();
 //       }

 //       [HttpPost]
 //       [ValidateAntiForgeryToken]
 //       public ActionResult SignIn(user model, string ReturnUrl)
 //       {
 //           ViewBag.ReturnUrl = ReturnUrl;
 //           return View();
 //       }
