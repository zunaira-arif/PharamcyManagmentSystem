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
        NextOrderStatus newstatus = new NextOrderStatus();
        PharmacyDAL pdal = new PharmacyDAL();

        public ActionResult OrderIndex()
        {                
            ViewData["Orders"] = getorderEmployee();
            ViewData["Status"] = newstatus.GetNextOrderStatus(0);
            return View(ViewData["Orders"]);
        }
       
        public ActionResult Edit(int? id)
        {
             this.Session["OrderID"] = id; // to  set curunt order ID
             this.Session["EmpID"] = 1; //will be deleted after login mantainens
             int employeeID = int.Parse(Session["EmpID"].ToString());
            if (id == null)
            {
                return HttpNotFound();
            }
            var orderData = pdal.getOrderByEmployeeAndOrderId(employeeID, id);
            if (orderData.Count < 1 || orderData == null)
            {
                return HttpNotFound();
            }
            if (orderData[0].orderStatusId == 5)
            {

                return RedirectToAction("OrderIndex");
            }
            if (orderData[0].orderStatusId == 4)
            {
                return RedirectToAction("OrderIndex");
            }          
            ViewData["employeeName"] = orderData[0].employee.firstName;
            ViewData["Orderdate"] = orderData[0].orderDate;
            ViewData["OldStatus"] = orderData[0].orderstatu.statusName;
            ViewBag.orderStatus = newstatus.GetNextOrderStatus(orderData[0].orderStatusId); 
            return View();
        }
        public ActionResult DeleteItem(int id)
        {
            pdal.DeletItemFromOrder(id);
            int? orderID=int.Parse(this.Session["OrderID"].ToString());
            return RedirectToAction("LoadCategory/"+orderID);
        
        }
        public ActionResult SaveItem(int id)
        {
            pdal.SaveItem(id);
            int? orderID = int.Parse(this.Session["OrderID"].ToString());
            return RedirectToAction("LoadCategory/" + orderID);
        
        }
        public ActionResult ReciveOrder(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            this.Session["OrderID"] = id;
            List<OrderTableStructure> itemList = pdal.GetOrderDetails(id);
            ViewData["orderItemS"] = itemList;
            return View(ViewData["orderItemS"]);
        }
      
        #region Addnewitems
        public ActionResult LoadCategory(int? id)
        {
            List<OrderTableStructure> itemList = pdal.GetOrderDetails(id);
            ViewData["orderItemS"] = itemList;
            if (id == null)
            {
               return HttpNotFound();
            }
            this.Session["OrderID"] = id; // to  set curunt order ID
            int emp=int.Parse(this.Session["EmpID"].ToString());
            var orderData = pdal.getOrderByEmployeeAndOrderId(emp, id);
            if (orderData.Count < 1 || orderData == null)
            {
                return HttpNotFound();
            }
            ViewData["C_O_S"] = pdal.GetCruntOrderStatus(id);
            if (orderData[0].orderStatusId == 1)
            {                
                ViewData["Category"] = pdal.GetCategory();
                return View(ViewData["orderItemS"]);
            }
            else {               
                return View(ViewData["orderItemS"]);
            }                    
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
        #endregion

       #region Order Part
       public List<PharmacyManagmentSystem.Models.order> getorderEmployee()
       {
           this.Session["userName"] = "Loged in user";
           this.Session["EmpID"] = 1; //will be deleted after login mantainens
           int employeeID = int.Parse(Session["EmpID"].ToString());
           List<PharmacyManagmentSystem.Models.order> list = pdal.getOrderByEmployee(employeeID);
           return list;
       }
        public JsonResult GetOrders( )
        {
            string orderID = this.Session["OrderID"].ToString();
            List<OrderTableStructure> itemList = pdal.GetOrderDetails(int.Parse(orderID));
            return Json(itemList,JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetOrderStatus(int? id)
        {
            SelectList list = pdal.GetOrderStatus();
            return Json(list);
        }
        public string GetCurntOrderStatus(int? id)
        {
            string status = pdal.GetCruntOrderStatus(id);
            return status;
        }
        
        public JsonResult AddOrderHistoy(string StatusChanged, string discription)
        {            
            try {
                int orderid = int.Parse(this.Session["OrderID"].ToString());
                int? empID = int.Parse(this.Session["EmpID"].ToString());
                DateTime date = DateTime.Now;
                pdal.AddOrderHistory(StatusChanged, discription, orderid, empID,date);
                return Json("ok");
            }
            catch(Exception e){
                return Json("Not Saved" + e.Data.ToString());
            }
           
        
        }

        public JsonResult ChangeOrderStatus(int newstatusid)
        {
            try
            {
                int orderid = int.Parse(this.Session["OrderID"].ToString());
                pdal.ChangeOrderStatus(orderid, newstatusid);
                return Json("ok");
            }
            catch (Exception e)
            {
                return Json("not ok");            
            }
           
        }
       
        public JsonResult SaveNewOrder(string Date)
        {          
         DateTime newdate=DateTime.Today;
         if (DateTime.TryParse(Date, out newdate))
         {
             int id = int.Parse(this.Session["EmpID"].ToString());
             pdal.AddNewOrder(newdate,id);
             return Json("ok");
         }
         return Json("not ok");
        }
       #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
               pdal.Dispose();
               newstatus = null;
            }
            base.Dispose(disposing);
        } 
        
    }
}