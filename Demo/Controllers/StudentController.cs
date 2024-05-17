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
    public class StudentController : Controller
    {
        private readonly DB db;
        private readonly IWebHostEnvironment en;
        private readonly Helper hp;

        public StudentController(DB db, IWebHostEnvironment en, Helper hp)
        {
            this.db = db;
            this.en = en;
            this.hp = hp;
        }

        public IActionResult Index()
        {
            ViewBag.studentList = db.Students;
            return View();
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Student/Create
        [HttpPost]
        public ActionResult Create(CreateStudentsVM vm)
        {
            if (ModelState.IsValid)
            {
                db.Tutors.Add(new()
                {
                    Id = vm.Id,
                    Email = vm.Email,
                    Hash = vm.Hash,
                    Name = vm.Name,
                    Gender = vm.Gender,
                    PhotoURL = vm.PhotoURL,
                    Age = vm.Age,
                    Phone = vm.Phone,
                    //ClassesId = AssignType(vm.Age),

                });
                db.SaveChanges();
                TempData["Info"] = $"Student inserted.";
                //return RedirectToAction("Index");
            }

            ViewBag.Tutors = db.Tutors;
            return View(vm);
        }

        // GET: Student/AssignClass
        public IActionResult AssignClass(string? id)
        {
            var s = db.Students.Find(id);

            if (s == null)
            {
                return RedirectToAction("Index");
            }


            //dropdown list for class list
            ViewBag.ClassList = new SelectList(db.Classes.OrderBy(c => c.Id), "Id", "Name");
            return View(s);
        }


        [HttpPost]
        public IActionResult AssignClass(string id, string classId)
        {
            var s = db.Students.Find(id);

            if (s == null)
            {
                TempData["Info"] = $"Student {id} not found.";
                return RedirectToAction("Index");
            }

            // Update ClassId for the student
            s.ClassId = classId;

            // Save changes
            db.SaveChanges();

            TempData["Info"] = "Class assigned successfully.";
            return RedirectToAction("Index");
        }

        // GET: Student/ViewTimetable
        public IActionResult ViewTimetable(string id)
        {
            var student = db.Students
                            .Include(s => s.Class)
                                .ThenInclude(cs => cs.ClassSubjects)
                                    .ThenInclude(c => c.Subject)
                                        .ThenInclude(t => t.Tutor)
                            .FirstOrDefault(s => s.Id == id);

            if (student == null)
            {
                return RedirectToAction("Index");
            }

            return View(student);
        }

    }
}

