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
  
    public class TrainerTopicsController : Controller
    {
        private FptHATTEntities db = new FptHATTEntities();

        // GET: TrainerTopics
        public ActionResult Index()
        {
            var trainerTopics = db.TrainerTopics.Include(t => t.Topic).Include(t => t.Trainer);
            return View(trainerTopics.ToList());
        }

        // GET: TrainerTopics/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrainerTopic trainerTopic = db.TrainerTopics.Find(id);
            if (trainerTopic == null)
            {
                return HttpNotFound();
            }
            return View(trainerTopic);
        }

        // GET: TrainerTopics/Create
        public ActionResult Create()
        {
            ViewBag.TopicID = new SelectList(db.Topics, "TopicID", "TopicName");
            ViewBag.TrainerID = new SelectList(db.Trainers, "TrainerID", "TrainerExternal");
            return View();
        }

        // POST: TrainerTopics/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TrainerTopicID,TrainerID,TopicID,TrainerTopicDate")] TrainerTopic trainerTopic)
        {
            if (ModelState.IsValid)
            {
                db.TrainerTopics.Add(trainerTopic);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TopicID = new SelectList(db.Topics, "TopicID", "TopicName", trainerTopic.TopicID);
            ViewBag.TrainerID = new SelectList(db.Trainers, "TrainerID", "TrainerExternal", trainerTopic.TrainerID);
            return View(trainerTopic);
        }

        // GET: TrainerTopics/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrainerTopic trainerTopic = db.TrainerTopics.Find(id);
            if (trainerTopic == null)
            {
                return HttpNotFound();
            }
            ViewBag.TopicID = new SelectList(db.Topics, "TopicID", "TopicName", trainerTopic.TopicID);
            ViewBag.TrainerID = new SelectList(db.Trainers, "TrainerID", "TrainerExternal", trainerTopic.TrainerID);
            return View(trainerTopic);
        }

        // POST: TrainerTopics/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TrainerTopicID,TrainerID,TopicID,TrainerTopicDate")] TrainerTopic trainerTopic)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trainerTopic).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TopicID = new SelectList(db.Topics, "TopicID", "TopicName", trainerTopic.TopicID);
            ViewBag.TrainerID = new SelectList(db.Trainers, "TrainerID", "TrainerExternal", trainerTopic.TrainerID);
            return View(trainerTopic);
        }

        // GET: TrainerTopics/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrainerTopic trainerTopic = db.TrainerTopics.Find(id);
            if (trainerTopic == null)
            {
                return HttpNotFound();
            }
            return View(trainerTopic);
        }

        // POST: TrainerTopics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TrainerTopic trainerTopic = db.TrainerTopics.Find(id);
            db.TrainerTopics.Remove(trainerTopic);
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
