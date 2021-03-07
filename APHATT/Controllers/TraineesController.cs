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

    public class TraineesController : Controller
    {
        private FptHATTEntities db = new FptHATTEntities();
        [Authorize(Roles = "Staff")]
        // GET: Trainees
        public ActionResult Index(string name, string account, string age, string birth,string education, string language, string TOEIC, string ex, string department, string location)
        {
            var links = from l in db.Trainees 
                        select l;

            if (!String.IsNullOrEmpty(name)) 
            {
                links = links.Where(s => s.TraineeName.Contains(name)); 
            }
            if (!String.IsNullOrEmpty(account)) 
            {
                links = links.Where(s => s.TraineeAccounts.Contains(account)); 
            }


            if (!String.IsNullOrEmpty(age))
            {
                links = links.Where(s => s.TraineeAge.Contains(age));
            }


            if (!String.IsNullOrEmpty(birth))
            {
                links = links.Where(s => s.TraineeDateOfBirth.Contains(birth));
            }

            if (!String.IsNullOrEmpty(education))
            {
                links = links.Where(s => s.TraineeEducation.Contains(education));
            }

            if (!String.IsNullOrEmpty(language))
            {
                links = links.Where(s => s.TraineeMainProgrammingLanguage.Contains(language));
            }

            if (!String.IsNullOrEmpty(TOEIC))
            {
                links = links.Where(s => s.TraineeTOEIC.Contains(TOEIC));
            }
            if (!String.IsNullOrEmpty(ex))
            {
                links = links.Where(s => s.TraineeExperienceDetails.Contains(ex));
            }
            if (!String.IsNullOrEmpty(department))
            {
                links = links.Where(s => s.TraineeDepartment.Contains(department));
            }
            if (!String.IsNullOrEmpty(location))
            {
                links = links.Where(s => s.TraineeLocation.Contains(location));
            }
            return View(links); 
            
        }
        [Authorize(Roles = "Staff")]
        // GET: Trainees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trainee trainee = db.Trainees.Find(id);
            if (trainee == null)
            {
                return HttpNotFound();
            }
            return View(trainee);
        }
        [Authorize(Roles = "Staff")]

        // GET: Trainees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Trainees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TraineeID,TraineeName,TraineeAccounts,TraineeAge,TraineeDateOfBirth,TraineeEducation,TraineeMainProgrammingLanguage,TraineeTOEIC,TraineeExperienceDetails,TraineeDepartment,TraineeLocation")] Trainee trainee)
        {
            if (ModelState.IsValid)
            {
                db.Trainees.Add(trainee);
                db.SaveChanges();
                AuthenController.CreateAccount(trainee.TraineeAccounts, "HATT123", "Trainee");
                return RedirectToAction("Index");
            }

            return View(trainee);
        }
        [Authorize(Roles = "Staff")]
        // GET: Trainees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trainee trainee = db.Trainees.Find(id);
            if (trainee == null)
            {
                return HttpNotFound();
            }
            return View(trainee);
        }

        // POST: Trainees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TraineeID,TraineeName,TraineeAccounts,TraineeAge,TraineeDateOfBirth,TraineeEducation,TraineeMainProgrammingLanguage,TraineeTOEIC,TraineeExperienceDetails,TraineeDepartment,TraineeLocation")] Trainee trainee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trainee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(trainee);
        }
        [Authorize(Roles = "Staff")]
        // GET: Trainees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trainee trainee = db.Trainees.Find(id);
            if (trainee == null)
            {
                return HttpNotFound();
            }
            return View(trainee);
        }

        // POST: Trainees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Trainee trainee = db.Trainees.Find(id);
            db.Trainees.Remove(trainee);
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
