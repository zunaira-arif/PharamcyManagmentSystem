using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PharmacyManagmentSystem.Models;

namespace PharmacyManagmentSystem.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Login(user  model, string returnUrl)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = await UserManager.FindAsync(model.UserName, model.Password);
        //        if (user != null)
        //        {
                   
        //            await SignInAsync(user, model.RememberMe);
        //            return RedirectToLocal(returnUrl);
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", "Invalid username or password.");
        //        }
        //    }

        //    // If we got this far, something failed, redisplay form
        //    return View(model);
        //}




	}
}