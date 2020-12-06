using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project1.Models;
using Project1.ViewModels;

namespace Project1.Controllers
{
   
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext db;

        UserManager<ApplicationUser> userManager;
        RoleManager<IdentityRole> roleManager;

        public AdminController(ApplicationDbContext db,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            this.db = db;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        
        public IActionResult AddLesson()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddLesson(Lesson lesson)
        {
            db.Add(lesson);
            await db.SaveChangesAsync();
            return RedirectToAction("AddLesson");
        }


        public async Task<IActionResult> AllLesson()
        {
            var lesson = await db.Lessons.ToListAsync();
            return View(lesson);
        }


        public IActionResult AddRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(IdentityRole role)
        {
            var result = await roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                return RedirectToAction("AllRole");
            }
            return View();
        }

        public IActionResult AllRole()
        {
            var roles = roleManager.Roles.ToList();
            return View(roles);
        }

        public async Task<IActionResult> AssignRole(string id)
        {
            var roleDisplay = db.Roles.Select(x => new
            {
                Id = x.Id,
                Value = x.Name
            }).ToList();
            RoleAddUserRoleViewModel vm = new RoleAddUserRoleViewModel();
            var user = await userManager.FindByIdAsync(id);
            vm.User = user;
            vm.RoleList = new SelectList(roleDisplay, "Id", "Value");
            return View(vm);
        }


        [HttpPost]
        public async Task<IActionResult> AssignRole
           (RoleAddUserRoleViewModel vm)
        {
            var user = await userManager.FindByIdAsync(vm.User.Id);
            var role = await roleManager.FindByIdAsync(vm.Role);
            var result = await userManager.
                AddToRoleAsync(user, role.Name);
            if (result.Succeeded)
            {
                return RedirectToAction("Account", "AllUser");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.Code,
                    error.Description);
            }
            var roleDisplay = db.Roles.Select(x => new
            {
                Id = x.Id,
                Value = x.Name
            }).ToList();
            vm.User = user;
            vm.RoleList = new SelectList(roleDisplay, "Id", "Value");
            return View(vm);
        }


        public IActionResult Index()
        {
            ViewBag.AmeeTest = "AmeeTest";
            ViewBag.Test2 = "Test2";
            ViewBag.Test3 = "Test3";
            return View();
        }
    }
}
