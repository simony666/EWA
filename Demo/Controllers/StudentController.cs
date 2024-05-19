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
            ViewBag.ClassList = db.Classes.ToDictionary(c => c.Id, c => c.Name);
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
            // Find the student by ID
            var student = db.Students.Find(id);

            if (student == null)
            {
                TempData["Info"] = $"Student {id} not found.";
                return RedirectToAction("Index");
            }

            // Find the class by classId
            var selectedClass = db.Classes.Find(classId);

            if (selectedClass == null)
            {
                TempData["Info"] = $"Class {classId} not found.";
                return RedirectToAction("Index");
            }

            // Validate age for the class
            bool isValidAge = false;
            switch (selectedClass.ClassType)
            {
                case "K1":
                    isValidAge = student.Age >= 3 && student.Age <= 4;
                    break;
                case "K2":
                    isValidAge = student.Age == 5;
                    break;
                case "K3":
                    isValidAge = student.Age == 6;
                    break;
                default:
                    TempData["Info"] = "Invalid class type.";
                    return RedirectToAction("Index");
            }

            // If age is not valid for the selected class, return with error message
            if (!isValidAge)
            {
                TempData["Info"] = $"Student age ({student.Age} years old) is not within the allowed range for class {selectedClass.Name}.";
                return RedirectToAction("Index");
            }

            // Check if class capacity will be exceeded (only allows 20 students in a class)
            if (selectedClass.Capacity > 21)
            {
                TempData["Info"] = $"{selectedClass.Name} is fully occupied. Please select a new class or create a new class to assign students.";
                return RedirectToAction("Index");
            }

            // Increase the class capacity by 1
            selectedClass.Capacity += 1;

            // Update ClassId for the student
            student.ClassId = classId;

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

