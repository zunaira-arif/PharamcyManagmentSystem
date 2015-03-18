using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PharmacyManagmentSystem.Models;
using PharmacyManagmentSystem.DAL;
namespace PharmacyManagmentSystem.Controllers
{
    public class DepartmentController : Controller
    {
        //private pharmacyEntities db = new pharmacyEntities();

        PharmacyDAL pdal = new PharmacyDAL();
        // GET: /Department/
        public ActionResult Index()
        {
            return View(pdal.GetDepartments());
        }

        // GET: /Department/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            department department = pdal.FindDepartment(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // GET: /Department/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Department/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="departmentId,name,description,deleted")] department department)
        {
            if (ModelState.IsValid)
            {
                pdal.AddNewDeprtment(department);
                return RedirectToAction("Index");
            }

            return View(department);
        }

        // GET: /Department/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            department department = pdal.FindDepartment(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // POST: /Department/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="departmentId,name,description,deleted")] department department)
        {
            if (ModelState.IsValid)
            {
                pdal.EditDepartment(department);
                return RedirectToAction("Index");
            }
            return View(department);
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                pdal.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
