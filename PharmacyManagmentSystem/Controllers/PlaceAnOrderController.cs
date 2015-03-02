using PharmacyManagmentSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PharmacyManagmentSystem.DAL;
namespace PharmacyManagmentSystem.Controllers
{
    public class PlaceAnOrderController : Controller
    {
        PharmacyDAL pdal = new PharmacyDAL();
               
        public ActionResult LoadCategory()
        {
            ViewData["Category"] = pdal.GetCategory();
            return View();
        }
        
        public JsonResult GetProduct(string id)
        {          
            return Json( pdal.GetProduct(id));
        }

        public JsonResult GetProductSize(string id)
        {

            return Json(pdal.GetProductSize(id));
         }

        public JsonResult GetSupplier(string id)
        {
            return Json(pdal.GetSupplier(id));
        }

        public JsonResult GetUnit(string id)
        {
            return Json(pdal.GetUnit(id));
         }
       
	}
}