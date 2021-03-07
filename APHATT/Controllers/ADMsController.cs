using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using APHATT.Models;

namespace APHATT.Controllers
{
    [Authorize]
    public class ADMsController : Controller
    {
        private FptHATTEntities db = new FptHATTEntities();
        [Authorize(Roles = "Admin")]
        // GET: ADMs
        public ActionResult Index()
        {
            return View(db.ADMs.ToList());
        }
        [Authorize(Roles = "Admin")]
        // GET: ADMs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ADM aDM = db.ADMs.Find(id);
            if (aDM == null)
            {
                return HttpNotFound();
            }
            return View(aDM);
        }
        [Authorize(Roles = "Admin")]
        // GET: ADMs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ADMs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ADMID,ADMAccount,pass")] ADM aDM)
        {
            if (ModelState.IsValid)
            {
                db.ADMs.Add(aDM);
                db.SaveChanges();
                AuthenController.CreateAccount(aDM.ADMAccount, aDM.pass, "Admin");
                return RedirectToAction("Index");
            }

            return View(aDM);
        }
        [Authorize(Roles = "Admin")]
        // GET: ADMs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ADM aDM = db.ADMs.Find(id);
            if (aDM == null)
            {
                return HttpNotFound();
            }
            return View(aDM);
        }
        [Authorize(Roles = "Admin")]
        // POST: ADMs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ADMID,ADMAccount,pass")] ADM aDM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aDM).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(aDM);
        }
        [Authorize(Roles = "Admin")]
        // GET: ADMs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ADM aDM = db.ADMs.Find(id);
            if (aDM == null)
            {
                return HttpNotFound();
            }
            return View(aDM);
        }

        // POST: ADMs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ADM aDM = db.ADMs.Find(id);
            db.ADMs.Remove(aDM);
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
