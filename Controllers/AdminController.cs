using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project1.Models;

namespace Project1.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext db;

        public AdminController(ApplicationDbContext db)
        {
            this.db = db;
        }
        public IActionResult AddLesson()
        {
            Lesson lesson = new Lesson();
            var currentUserId = this.User.FindFirst
                (ClaimTypes.NameIdentifier).Value;
            lesson.CoachId = db.Coachs.
                SingleOrDefault(i => i.UserId ==
                currentUserId).CoachId;
            return View(lesson);
        }
        [HttpPost]
        public async Task<IActionResult> AddLesson(Lesson lesson)
        {
            db.Add(lesson);
            await db.SaveChangesAsync();
            return RedirectToAction("AllLesson");
        }
        public async Task<IActionResult> AllLesson()
        {
            var lesson = await db.Lessons.Include
                (c => c.Coach).ToListAsync();
            return View(lesson);
        }
        public IActionResult Index()
        {
            ViewBag.AmeeTest = "AmeeTest";
            return View();
        }
    }
}
