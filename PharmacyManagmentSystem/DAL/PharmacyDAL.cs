﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using PharmacyManagmentSystem.Models;
using System.Web.Mvc;

namespace PharmacyManagmentSystem.DAL
{
    public class PharmacyDAL
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
        public SelectList AddOrder(string prodetaiID, string suplierID, string Quantity, int empId)
        {
            int ProdDetailID = int.Parse(prodetaiID);
            int SupplierID = int.Parse(suplierID);
            int QuantityOrder = int.Parse(Quantity);
            var getProSuppliedID = db.productsupplieds.Where(p => p.productDetailId == ProdDetailID && p.supplierId==SupplierID).FirstOrDefault();
            int ProSuppliedID = getProSuppliedID.productSuppliedId;
            /////create an order////////
            var order = new order();
            order.empId = empId;
            order.orderDate=DateTime.Today;
            order.orderStatus=1;
            ///save an or der/////////////
            db.orders.Add(order);
            db.SaveChanges();
            ////create product order ////////////////
            var productorderd = new productsorderd();
            productorderd.orderId= 1;//getorderid
            productorderd.ProductSupplied_productSuppliedId = ProSuppliedID;
            ///////////////save product order
            db.productsorderds.Add(productorderd);
            db.SaveChanges();


            SelectList list = new SelectList(db.orders.ToString());
            return list;
        }
        public SelectList AddOrderDetails(string prodetaiID, string suplierID, string Quantity, int empId, int orderID)
        {
            int ProdDetailID = int.Parse(prodetaiID);
            int SupplierID = int.Parse(suplierID);
            int QuantityOrder = int.Parse(Quantity);
            var getProSuppliedID = db.productsupplieds.Where(p => p.productDetailId == ProdDetailID && p.supplierId == SupplierID).FirstOrDefault();
            int ProSuppliedID = getProSuppliedID.productSuppliedId;
          
            ////create product order ////////////////
            var productorderd = new productsorderd();
            productorderd.orderId =orderID;
            productorderd.ProductSupplied_productSuppliedId = ProSuppliedID;
            ///////////////save product order
            db.productsorderds.Add(productorderd);
            db.SaveChanges();
            ////create order detail ////////////////
            var getProOrderID =db.productsorderds.Where(p => p.orderId== orderID && p.ProductSupplied_productSuppliedId==ProSuppliedID).FirstOrDefault();
            int ProOrderID = getProOrderID.productsOrderdId;
            var orderdetailItems = new orderdetail();
            orderdetailItems.quantityOrderd = QuantityOrder;
            orderdetailItems.productsOrderdId = ProOrderID;
           // orderdetail;
            ///////////////save order detail
            db.orderdetails.Add(orderdetailItems);
            db.SaveChanges();
            SelectList list = new SelectList(db.orders.ToString());
            return list;
        }
    }    
}
