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
            SelectList list=new SelectList(db.productsupplieds.Where(c => c.productDetailId == ID), "supplierId", "supplierName");
            return list;
        }

        public SelectList GetUnit(string id)
        {
            var ID = int.Parse(id);
            SelectList list = new SelectList(db.categories.Where(c => c.categoryId == ID), "categoryId", "categoryUnit");
            return list;
        }
    }
    
}