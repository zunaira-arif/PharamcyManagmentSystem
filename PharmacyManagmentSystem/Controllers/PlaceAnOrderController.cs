using PharmacyManagmentSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PharmacyManagmentSystem.Controllers
{
    public class PlaceAnOrderController : Controller
    {
        private pharmacyEntities db = new pharmacyEntities();
        
        public ActionResult LoadCategory()
        {
            ViewData["Category"] = new SelectList(db.categories, "catadoryId", "categoryName");
            return View();
        }
        
        public JsonResult GetProduct(string id)
        {
            var ID = int.Parse(id);
            return Json(new SelectList(db.products.Where(p => p.catadoryId == ID), "productId", "productName"));
        }

        public JsonResult GetProductSize(string id)
        {
            var ID = int.Parse(id);
            return Json(new SelectList(db.productdetails.Where(p => p.productId == ID), "productDetailId", "productSize"));
         }

	}
}