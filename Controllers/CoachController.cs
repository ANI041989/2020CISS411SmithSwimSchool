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
            var lesson = await db.Lessons.ToListAsync();
            return View(lesson);
        }


        public IActionResult AddSession()
        {
            Session session = new Session();
            var coachId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            session.CoachId = db.Coachs.SingleOrDefault(i => i.UserId == coachId).CoachId;
            return View(session);
           
        }

        [HttpPost]
        public async Task<IActionResult> AddSession(Session session)
        {
            db.Add(session);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "Coach");
        }


        public async Task<IActionResult> AllSession()
        {
            var session = await db.Sessions.Include
                 (s => s.Coach).ToListAsync();
            return View(session);
        }


        public IActionResult Index()
        {
            return View();
        }





        public async Task<IActionResult> SessionByCoach()
        {
            var currentUserId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var CoachId = db.Coachs.SingleOrDefault(i => i.UserId == currentUserId).CoachId;
            var session = await db.Sessions.Where(i => i.CoachId == CoachId).ToListAsync();
            return View(session);
        }


        public async Task<IActionResult> PostGrade(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var allSwimmers = await db.Enrollments.Include(c => c.Session).Where(c => c.SessionId == id).ToListAsync();
            if (allSwimmers == null)
            {
                return NotFound();
            }
            return View(allSwimmers);

        }

        [HttpPost]
        public IActionResult PostGrade(List<Enrollment> enrollments)
        {
            foreach (var enrollment in enrollments)
            {
                var er = db.Enrollments.Find(enrollment.EnrollmentId);
                er.LetterGrade = enrollment.LetterGrade;
            }
            db.SaveChanges();
            return RedirectToAction("SessionByCoach");

        }
    }
}
