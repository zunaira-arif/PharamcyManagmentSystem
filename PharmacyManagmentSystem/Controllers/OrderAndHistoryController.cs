using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PharmacyManagmentSystem.DAL;
namespace PharmacyManagmentSystem.Controllers
{
    public class OrderAndHistoryController : Controller
    {
        PharmacyDAL pdal = new PharmacyDAL();
        public ActionResult OrderIndex()
        {
            this.Session.Timeout = 1000;
            ViewData["Orders"] = getorderEmployee();
            return View();
        }
        public ActionResult Edit(int? id)
        {
            this.Session["OrderID"] = id; // to  set curunt order ID
            if (id == null)
            {
                return View();
            }
           // ViewData["Orders"] = getorderEmployee();
            return View();
        }
       
        public List<PharmacyManagmentSystem.Models.order> getorderEmployee()
        {
           this.Session["EmpID"] = 2; //will be deleted after login mantainens
           int employeeID = int.Parse(Session["EmpID"].ToString());
           List<PharmacyManagmentSystem.Models.order>  list = pdal.getOrderByEmployee(employeeID);
            return list;
        }
        public ActionResult LoadCategory(int? id)
        { 
            this.Session["OrderID"] = id; // to  set curunt order ID
            if (id==null)
        {
            ViewData["Category"] = pdal.GetCategory();
            return View();
        }
            ViewData["Category"] = pdal.GetCategory();
            return View();
        }

        public JsonResult GetProduct(string id)
        {
            return Json(pdal.GetProduct(id));
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
            string orderID = this.Session["OrderID"].ToString();
            return Json(pdal.AddOrderDetails(ProductSuppliedID, SupplierID, quantity, int.Parse(empId), int.Parse(orderID)));
            }

        public JsonResult GetOrders( )
        {
            string orderID = this.Session["OrderID"].ToString();
            List<OrderTableStructure> itemList = pdal.GetOrderDetails(int.Parse(orderID));
            return Json(itemList, JsonRequestBehavior.AllowGet);
        }

    }
}