using Demo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace Demo.Controllers
{
    public class TutorController : Controller
    {
        private readonly DB db;
        private readonly IWebHostEnvironment en;
        private readonly Helper hp;

        public TutorController(DB db, IWebHostEnvironment en, Helper hp)
        {
            this.db = db;
            this.en = en;
            this.hp = hp;
        }

        public ActionResult Create()
        {
            return View();
        }


            // POST: Tutor/Create
            [HttpPost]
        public ActionResult Create(CreateTutorsVM vm)
        {
            if (ModelState.IsValid)
            {
                db.Tutors.Add(new()
                {
                    Id = vm.Id,
                    Email = vm.Email,
                    Hash = vm.Hash,
                    Name = vm.Name,
                    Gender =vm.Gender,
                    PhotoURL = vm.PhotoURL,
                    Age = vm.Age,
                    Phone = vm.Phone,
                   
                });
                db.SaveChanges();
                TempData["Info"] = $"Tutors inserted.";
                //return RedirectToAction("Index");
            }

            ViewBag.Tutors = db.Tutors;
            return View(vm);
        }

        // GET: Tutor/AssignClass
        public IActionResult AssignClass()
        {
            ViewBag.Tutors = db.Tutors;
            return View();
        }

        // GET: Tutor/ViewTimetable
        public IActionResult ViewTimetable(string id)
        {
            var tutorClasses = db.Tutors
                      .Include(t => t.Subjects)
                          .ThenInclude(cs => cs.ClassesSubjects)
                              .ThenInclude(c => c.Class)
                      .FirstOrDefault(t => t.Id == id);

            if (tutorClasses == null)
            {
                return RedirectToAction("Index");
            }
            return View(tutorClasses);
        }
    }
}
