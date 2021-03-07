using APHATT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace APHATT.Controllers
{
    public class StudentActionController : Controller
    {
        private FptHATTEntities db = new FptHATTEntities();
        // GET: StudentAction
        public ActionResult Profile()
        {
            var userName = User.Identity.Name;
            var student = (from s in db.Trainees
                           where s.TraineeAccounts.Equals(userName)
                           select s
                           ).FirstOrDefault();

            return View(student);
        }
        public ActionResult MyCourses()
        {
            var userName = User.Identity.Name;
            var traineeq = (from s in db.Trainees
                           where s.TraineeAccounts.Equals(userName)
                           select s
                           ).FirstOrDefault();

            var enrollment1 = from s1 in db.Enrollments
                              where s1.TraineeID == traineeq.TraineeID
                              select s1;
            return View(traineeq.Enrollments.ToList());
        }
    }
}