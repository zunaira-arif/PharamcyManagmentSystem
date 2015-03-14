using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using PharmacyManagmentSystem.Models;
using System.Web.Mvc;

namespace PharmacyManagmentSystem.DAL
{
    public class PharmacyDAL : Controller 
    {
        private pharmacyEntities db = new pharmacyEntities();
        
        public SelectList GetCategory()
        {
                 return new SelectList(db.categories, "categoryId", "categoryName");
        }
        public SelectList GetProduct(string id)
        {
            var ID = int.Parse(id);
            SelectList list= new SelectList(db.products.Where(p => p.categoryId == ID), "productId", "productName");
            return list;
        }
        public SelectList GetProductSize(string id)
        {
            var ID = int.Parse(id);
            SelectList list = new SelectList(db.productdetails.Where(p => p.productId == ID), "productDetailId", "productSize");
            return list;
        }
        public SelectList GetSupplier(string id)
        {
            var ID = int.Parse(id);
            IQueryable<productsupplied> outer = db.productsupplieds;
            IQueryable<supplier> inner = db.suppliers;
            var results = outer.Where(product => product.productDetailId == ID)
                               .Join(
                                    inner,
                                    product => product.supplierId,
                                    supplier => supplier.supplierId,
                                    (product, supplier) => new
                                    {
                                        supplierID = supplier.supplierId,
                                        Suppliername = supplier.supplierName
                                    });
            SelectList list = new SelectList(results, "supplierID", "Suppliername");
            return list;
        }
        public SelectList GetUnit(string id)
        {
            var ID = int.Parse(id);
            SelectList list = new SelectList(db.categories.Where(c => c.categoryId == ID), "categoryId", "categoryUnit");
            return list;
        }
        //public SelectList AddOrder(string prodetaiID, string suplierID, string Quantity, int empId)
        //{
        //    int ProdDetailID = int.Parse(prodetaiID);
        //    int SupplierID = int.Parse(suplierID);
        //    int QuantityOrder = int.Parse(Quantity);
        //    var getProSuppliedID = db.productsupplieds.Where(p => p.productDetailId == ProdDetailID && p.supplierId==SupplierID).FirstOrDefault();
        //    int ProSuppliedID = getProSuppliedID.productSuppliedId;
        //    /////create an order////////
        //    var order = new order();
        //    order.empId = empId;
        //    order.orderDate=DateTime.Today;
        //    order.orderStatusId=1;
        //    ///save an or der/////////////
        //    db.orders.Add(order);
        //    db.SaveChanges();
        //    ////create product order ////////////////
        //    var productorderd = new productsorderd();
        //    productorderd.orderId= 1;//getorderid
        //    productorderd.productSuppliedId = ProSuppliedID;
        //    ///////////////save product order
        //    db.productsorderds.Add(productorderd);
        //    db.SaveChanges();


        //    SelectList list = new SelectList(db.orders.ToString());
        //    return list;
        //}
    
        public SelectList AddOrderDetails(string prodetaiID, string suplierID, string Quantity, int empId, int orderID)
        {
            int ProdDetailID = int.Parse(prodetaiID);
            int SupplierID = int.Parse(suplierID);
            int QuantityOrder = int.Parse(Quantity);
            var getProSuppliedID = db.productsupplieds.Where(p => p.productDetailId == ProdDetailID && p.supplierId == SupplierID).FirstOrDefault();
            int ProSuppliedID = getProSuppliedID.productSuppliedId;
            int al = AlreadyExsist(ProSuppliedID, orderID);
            if(al != 0)
            {
                var getOldRow = db.orderdetails.Where(o => o.productSuppliedId == ProSuppliedID&& o.orderId==orderID).FirstOrDefault(); ;
                int? oldQuantity = getOldRow.quantityOrderd;
                int? newQuantity = oldQuantity + QuantityOrder;
                getOldRow.quantityOrderd = newQuantity;
                 ///////////////save order detail
                db.SaveChanges();
            }
            else
            {
                ////create order detail ////////////////
                var orderdetailItems = new orderdetail();
                orderdetailItems.quantityOrderd = QuantityOrder;
                orderdetailItems.orderId = orderID;
                orderdetailItems.productSuppliedId = ProSuppliedID;
                ///////////////save order detail
                db.orderdetails.Add(orderdetailItems);
                db.SaveChanges();
            }
           
            SelectList list = new SelectList(db.orders.ToString());
            return list;
        }
        public int AlreadyExsist(int prosupID, int ordID)
        {
            var chkAlready = db.orderdetails.Where(p => p.orderId == ordID && p.productSuppliedId == prosupID).FirstOrDefault();

            if (chkAlready==null)
            {
                return 0;
            }
            else
            {
                return chkAlready.orderDetailId;
            }

        }
        
        #region Order-Part
        public void AddNewOrder(DateTime newdate, int id)
        {
            order neworder = new order();
            neworder.orderDate = newdate;
            neworder.orderStatusId = 1;
            neworder.empId = id;
            db.orders.Add(neworder);
            db.SaveChanges();
            AddOrderHistory("Draft", "New Order", neworder.orderId, id,newdate);
        }
        public SelectList GetOrderStatus()
        {
            SelectList list=new SelectList(db.orderstatus, "orderStatusId", "statusName");

            return list;
        }
        public List<order> getOrderByEmployee(int employeeID)
        {
            List<order> list = new List<order>(db.orders.Where(o => o.empId == employeeID).OrderByDescending(o=> o.orderDate));
            return list;
        }
        public List<order> getOrderByEmployeeAndOrderId(int employeeID,int? orderID)
        {
            List<order> list = new List<order>(db.orders.Where(o => o.empId == employeeID && o.orderId==orderID));
            return list;
        }
       
        public List<OrderTableStructure> GetOrderDetails(int? orderID)
        {  //List<OrderTableStructure> list =new List<OrderTableStructure>() ;
            //List<int> productOrderIdz=new List<int>();
            List<int>  productSuppliedIdz=new List<int>();
            OrderTableStructure ordertable=new OrderTableStructure();
            var data = db.orderdetails.Where(p => p.orderId == orderID);
            foreach (orderdetail po in data)
            {
                productSuppliedIdz.Add(po.productSuppliedId);
                //productOrderIdz.Add(po.productsOrderdId);
            }
            data = null;
            List<OrderTableStructure> list = new List<OrderTableStructure>();
            for (int idz = 0; idz < productSuppliedIdz.Count; idz++)
            {
                ordertable = new OrderTableStructure();
                ordertable.Id=idz+1;
                int O_ID=(int)orderID;
                int proSupID=productSuppliedIdz[idz];
                var qun = db.orderdetails.Where(p => p.productSuppliedId ==proSupID  && p.orderId == O_ID).SingleOrDefault();
                ordertable.Quantity = qun.quantityOrderd;
                ordertable.P_o_ID = qun.orderDetailId;
                qun = null;
                int PSid=productSuppliedIdz[idz];
                var sup = db.productsupplieds.Where(s => s.productSuppliedId == PSid).SingleOrDefault();
                int suplierId = sup.supplierId;
                int productdetailId = sup.productDetailId;
                sup = null;
                var supname = db.suppliers.Where(s => s.supplierId == suplierId).FirstOrDefault();
                ordertable.SupplierName = supname.supplierName;
                supname = null;
                var proSize = db.productdetails.Where(p => p.productDetailId == productdetailId).SingleOrDefault();
                ordertable.Size = proSize.productSize;
                int PNid = proSize.productId;
                proSize = null;
                var proName = db.products.Where(p => p.productId == PNid).SingleOrDefault();
                ordertable.ProductName1 = proName.productName;
                int catID = proName.categoryId;
                proName = null;
                var cat = db.categories.Where(c => c.categoryId == catID).SingleOrDefault();
                ordertable.CategoryName = cat.categoryName;
                cat = null;
                list.Add(ordertable);
              }
                return list;              
        }
       
        public void AddOrderHistory(string StatusChanged, string discription, int orderid, int? empID , DateTime date)
        {
        date = DateTime.Now;
        orderhistory orderhistory = new orderhistory();
        orderhistory.date = date;
        orderhistory.statusChanged = StatusChanged;
        orderhistory.discription = discription;
        orderhistory.orderId = orderid;
        orderhistory.employeeId = empID;
        db.orderhistories.Add(orderhistory);
        db.SaveChanges();

        }
        public void ChangeOrderStatus(int Oid, int newStatusId)
        {
            var ordertoUpdate = db.orders.Find(Oid);
            ordertoUpdate.orderStatusId = newStatusId;
            db.SaveChanges();           
        }
        public void DeletItemFromOrder(int id)
        {
           orderdetail OD = db.orderdetails.Find(id);
           db.orderdetails.Remove(OD);
           db.SaveChanges();
           int i = 0;        
        }

        #endregion
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        } 
        
    }    
}
