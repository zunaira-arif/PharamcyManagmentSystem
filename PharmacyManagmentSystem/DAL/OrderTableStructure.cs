using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PharmacyManagmentSystem.DAL
{
    public class OrderTableStructure
    {
        int p_o_ID;

        public int P_o_ID
        {
            get { return p_o_ID; }
            set { p_o_ID = value; }
        }

        int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        
        string categoryName;
        public string CategoryName
        {
            get { return categoryName; }
            set { categoryName = value; }
        }

        string ProductName;
        public string ProductName1
        {
            get { return ProductName; }
            set { ProductName = value; }
        }

        double size;
        public double Size
        {
            get { return size; }
            set { size = value; }
        }

        string supplierName;
        public string SupplierName
        {
            get { return supplierName; }
            set { supplierName = value; }
        }

        int? quantity;
        public int? Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }      
    }
}