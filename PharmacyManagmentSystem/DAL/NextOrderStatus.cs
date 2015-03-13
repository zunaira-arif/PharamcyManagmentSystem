using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PharmacyManagmentSystem.Models;
using System.Web.Mvc;
namespace PharmacyManagmentSystem.DAL
{
    public class NextOrderStatus
    {
        pharmacyEntities db = new pharmacyEntities();
        public SelectList GetNextOrderStatus(int? oldStatus)
        {
            SelectList NewStatus = null;
            
            //List<orderstatu > NewStatus = new List<orderstatu>();
            switch (oldStatus)
            {
                case 1:
                    NewStatus = new SelectList(db.orderstatus.Where(s => s.orderStatusId == 2 || s.orderStatusId == 5), "orderStatusId", "statusName");
                    break;

                case 2:
                    NewStatus = new SelectList(db.orderstatus.Where(s => s.orderStatusId == 3 || s.orderStatusId == 4 || s.orderStatusId == 5), "orderStatusId", "statusName");
                    break;

                case 3:
                    NewStatus = new SelectList(db.orderstatus.Where(s => s.orderStatusId == 4), "orderStatusId", "statusName");
                    break;
                case 4:
                    NewStatus = new SelectList(db.orderstatus.Where(s => s.orderStatusId == 4 ), "orderStatusId", "statusName");
                    break;
                case 5:
                    NewStatus = new SelectList(db.orderstatus.Where(s => s.orderStatusId == 5), "orderStatusId", "statusName");
                    break;
                default:
                    NewStatus = new SelectList(db.orderstatus.Where(s => s.orderStatusId == 1), "orderStatusId", "statusName");
                    break;
            }
            
            return NewStatus;
        
        }



    }
}