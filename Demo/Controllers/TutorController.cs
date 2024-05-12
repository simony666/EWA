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

        // GET: Tutor/AssignClass
        public IActionResult AssignClass()
        {
            ViewBag.Tutors = db.Tutors;
            return View();
        }

            // GET: Tutor/ViewClass
        public IActionResult ViewClass(string id)
        {
            var tutorClasses = db.Tutors
                      .Include(t => t.Subjects)
                          .ThenInclude(cs => cs.ClassesSubjects)
                              .ThenInclude(c => c.Classes)
                      .FirstOrDefault(t => t.Id == id);

            if (tutorClasses == null)
            {
                return RedirectToAction("Index");
            }
            return View(tutorClasses);
        }
    }
}
