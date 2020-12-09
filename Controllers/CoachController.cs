using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project1.Models;

namespace Project1.Controllers
{
    [Authorize(Roles="Coach")]
    public class CoachController : Controller
    {
        private readonly ApplicationDbContext db;

        public CoachController(ApplicationDbContext db)
        {
            this.db = db;
        }
        public IActionResult AddProfile()
        {
            var currentUserId = this.User.FindFirst
                (ClaimTypes.NameIdentifier).Value;
            Coach coach = new Coach();
            if (db.Coachs.Any(i => i.UserId ==
            currentUserId))
            {
                coach = db.Coachs.FirstOrDefault
                    (i => i.UserId == currentUserId);
            }
            else
            {
                coach.UserId = currentUserId;
            }

            return View(coach);

        }
        [HttpPost]
        public async Task<IActionResult> AddProfile
            (Coach coach)
        {
            var currentUserId = this.User.FindFirst
                (ClaimTypes.NameIdentifier).Value;
            if (db.Coachs.Any
                (i => i.UserId == currentUserId))
            {
                var coachToUpdate = db.Coachs.FirstOrDefault
                    (i => i.UserId == currentUserId);
                coachToUpdate.CoachName = coach.CoachName;
                coachToUpdate.Bio = coach.Bio;
                db.Update(coachToUpdate);
            }
            else
            {
                db.Add(coach);
            }
            await db.SaveChangesAsync();
            return View("Index");

        }

        public async Task<IActionResult> AllLesson()
        {
            var currentUserId = this.User.FindFirst
                (ClaimTypes.NameIdentifier).Value;
            var coachId = db.Coachs.SingleOrDefault
                (i => i.UserId == currentUserId).CoachId;
            var lesson = await db.Coachs.Where(i =>
            i.CoachId == coachId).ToListAsync();
            return View(lesson);
        }
        public async Task<IActionResult> AddSession(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var allSwimmers = await db.Enrollments.Include
                (c => c.lesson).Where(c => c.LessonId == id)
                .ToListAsync();
            if (allSwimmers == null)
            {
                return NotFound();
            }
            return View(allSwimmers);
        }
        [HttpPost]
        public IActionResult AddSession(List<Enrollment> enrollments)
        {
            foreach (var enrollment in enrollments)
            {
                var er = db.Enrollments.Find(enrollment.EnrollmentId);
                er.SessionTime = enrollment.SessionTime;
            }
            db.SaveChanges();
            return RedirectToAction("AllLesson");
        }

        public IActionResult WriteProgressReport()
        {
            return View();
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}
