using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PharmacyManagmentSystem.Models;
namespace PharmacyManagmentSystem.Controllers
{
    public class LoginController : Controller
    {
        pharmacyEntities db = new pharmacyEntities();
        // GET: Login

        public ActionResult Login()
        {
            return View();
        }
        public JsonResult SignIn(String id, String password)
        {

            return Json(LoginAthu(id, password));
        }
        public SelectList LoginAthu(String id, String password)
        {
            SelectList list = new SelectList(db.employees.ToList(),"LastName","gender");
            var user = db.users.Where(u=> u.userName==id && u.password==password).FirstOrDefault();
            if (user.enabled == true && !user.Equals("null"))
            { 
                this.Session["userName"]=id;
                this.Session["employeeid"]=id; //get employe id of the respective username 
                list = new SelectList(db.users.ToList(),"userName","password");
               
            }
            return list;
        }
    }

}