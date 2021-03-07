using APHATT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
namespace APHATT.Controllers
{
    public class TraineractionController : Controller
    {
        private FptHATTEntities db = new FptHATTEntities();
        // GET: Traineraction
        public ActionResult Profile()
        {
            var userName = User.Identity.Name;
            var teacher = (from s in db.Trainers
                           where s.TrainerTelephone.Equals(userName)
                           select s
                           ).FirstOrDefault();

            return View(teacher);
        }
        public ActionResult MyTask()
        {
            var trainerTopics = db.TrainerTopics.Include(e => e.Trainer).Include(e => e.Topic);
            var userName = User.Identity.Name;
            var traineeq = from s in trainerTopics
                           where s.Trainer.TrainerTelephone.Equals(userName)
                           select s;

            return View(traineeq.ToList());
        }
    }
}