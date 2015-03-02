using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PharmacyManagmentSystem.Models;

namespace PharmacyManagmentSystem.Controllers
{
    public class ProductSuppliedController : Controller
    {
        private pharmacyEntities db = new pharmacyEntities();

        // GET: /ProductSupplied/
        public ActionResult Index()
        {
            var productsupplieds = db.productsupplieds.Include(p => p.productdetail).Include(p => p.supplier);
            return View(productsupplieds.ToList());
        }

        // GET: /ProductSupplied/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            productsupplied productsupplied = db.productsupplieds.Find(id);
            if (productsupplied == null)
            {
                return HttpNotFound();
            }
            return View(productsupplied);
        }

        // GET: /ProductSupplied/Create
        public ActionResult Create()
        {
            ViewBag.productDetailId = new SelectList(db.productdetails, "productDetailId", "productDetailId");
            ViewBag.supplierId = new SelectList(db.suppliers, "supplierId", "supplierName");
            return View();
        }

        // POST: /ProductSupplied/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="productSuppliedId,productDetailId,supplierId")] productsupplied productsupplied)
        {
            if (ModelState.IsValid)
            {
                db.productsupplieds.Add(productsupplied);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.productDetailId = new SelectList(db.productdetails, "productDetailId", "productDetailId", productsupplied.productDetailId);
            ViewBag.supplierId = new SelectList(db.suppliers, "supplierId", "supplierName", productsupplied.supplierId);
            return View(productsupplied);
        }

        // GET: /ProductSupplied/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            productsupplied productsupplied = db.productsupplieds.Find(id);
            if (productsupplied == null)
            {
                return HttpNotFound();
            }
            ViewBag.productDetailId = new SelectList(db.productdetails, "productDetailId", "productDetailId", productsupplied.productDetailId);
            ViewBag.supplierId = new SelectList(db.suppliers, "supplierId", "supplierName", productsupplied.supplierId);
            return View(productsupplied);
        }

        // POST: /ProductSupplied/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="productSuppliedId,productDetailId,supplierId")] productsupplied productsupplied)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productsupplied).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.productDetailId = new SelectList(db.productdetails, "productDetailId", "productDetailId", productsupplied.productDetailId);
            ViewBag.supplierId = new SelectList(db.suppliers, "supplierId", "supplierName", productsupplied.supplierId);
            return View(productsupplied);
        }

        // GET: /ProductSupplied/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            productsupplied productsupplied = db.productsupplieds.Find(id);
            if (productsupplied == null)
            {
                return HttpNotFound();
            }
            return View(productsupplied);
        }

        // POST: /ProductSupplied/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            productsupplied productsupplied = db.productsupplieds.Find(id);
            db.productsupplieds.Remove(productsupplied);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

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
