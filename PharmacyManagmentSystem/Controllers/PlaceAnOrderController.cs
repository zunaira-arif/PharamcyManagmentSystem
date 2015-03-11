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
            this.Session["EmpID"] = 1;
            this.Session["orderId"] = 1;
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

       
        public JsonResult AddOrderDetail(string ProductSuppliedID, string SupplierID, string quantity)
        {
            
            string empId = this.Session["EmpID"].ToString();          
            string orderID = this.Session["orderId"].ToString();
          
                return Json(pdal.AddOrderDetails(ProductSuppliedID, SupplierID, quantity, int.Parse(empId), int.Parse(orderID)));
           }
	}
}