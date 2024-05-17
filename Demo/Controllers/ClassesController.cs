using Demo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace Demo.Controllers
{
    public class ClassesController : Controller
    {
        private readonly DB db;
        private readonly IWebHostEnvironment en;
        private readonly Helper hp;

        public ClassesController(DB db, IWebHostEnvironment en, Helper hp)
        {
            this.db = db;
            this.en = en;
            this.hp = hp;
        }

        public IActionResult Index()
        {
            var classes = db.Classes.ToList();
            var classViewModels = classes.Select(c => new ClassViewModel
            {
                Id = c.Id,
                Name = c.Name,
                ClassType = c.ClassType,
                Capacity = db.Students.Count(s => s.ClassId == c.Id) // Count students with matching ClassesId
            }).ToList();

            return View(classViewModels);
        }


        // GET: Classes/Create
        public ActionResult Create()
        {   
            return View();
        }

        // POST: Classes/Create
        [HttpPost]
        public ActionResult Create(ClassesVM vm)
        {
            if (ModelState.IsValid)
            {
                // Generate ID
                vm.Id = NextId();

                // Create a new class object
                var classes = new Class
                {
                    Id = vm.Id,
                    Name = vm.Name,
                    ClassType = vm.ClassType,
                };


                // Count the number of students enrolled in the class
                classes.Capacity = db.Classes.Count(c => c.Id == classes.Id);

                // Check if the capacity exceeds the limit
                if (classes.Capacity > 20)
                {
                    TempData["Info"] = $"Class {vm.Id} is fully occupied.";
                    //return RedirectToAction("Index");
                }

                // Add the class object to the context and save changes
                db.Classes.Add(classes);
                db.SaveChanges();

                TempData["Info"] = $"Class {vm.Id} inserted."; // Set Success message in TempData

                return RedirectToAction("Index");
            }

            return View(vm);
        }




        // Manually generate next id
        private string NextId()
        {
            string max = db.Classes.Max(c => c.Id) ?? "C000";
            int n = int.Parse(max[1..]);
            return (n + 1).ToString("'C'000");
        }


        // GET: Classes/Update
        //[Authorize(Roles = "Member")]
        public IActionResult Update(string id)
        {
            var c = db.Classes.Find(id);
            if (c != null)
            {
                var vm = new ClassesVM
                {
                    Id = c.Id,
                    Name = c.Name,
                    ClassType = c.ClassType,
                    Capacity = c.Capacity
                };
                return View(vm);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        //[Authorize(Roles = "Member")]
        [HttpPost]
        public IActionResult Update(ClassesVM vm)
        {
            if (ModelState.IsValid)
            {
                var c = db.Classes.Find(vm.Id);

                if (c != null)
                {
                    c.Name = vm.Name;
                    c.ClassType = vm.ClassType;
                    db.SaveChanges();
                    TempData["Info"] = "Class updated.";
                    return RedirectToAction("index");

                }
                else
                {
                    ModelState.AddModelError("Id", "Class not found.");
                }
            }
            else // If model state is invalid for POST request, reload the GET version of the page
            {
                return RedirectToAction("Update", new { id = vm.Id });
            }

            return View(vm);
        }

        // POST: Classes/Delete
        [HttpPost]
        public IActionResult Delete(string? id)
        {
            var c = db.Classes.Find(id);

            if (c != null)
            {
                // Check if any students are associated with this class
                var studentsWithClass = db.ClassesSubjects.Any(s => s.Class.Id.Contains(id));

                if (!studentsWithClass)
                {
                    db.Classes.Remove(c);
                    db.SaveChanges();
                    TempData["Info"] = $"Class {c.Id} deleted.";
                }
                else
                {
                    TempData["Info"] = $"Cannot delete class {c.Id} because there are students associated with it.";
                }
            }


            return RedirectToAction("Index");
        }


        // GET: Classes/ClassSubject
        public IActionResult ClassSubject()
        {
            ViewBag.ClassesList = new SelectList(db.Classes.OrderBy(t => t.Id), "Id", "Name");
            ViewBag.SubjectsList = new SelectList(db.Subjects.OrderBy(t => t.Id), "Id", "Name");
            return View();
        }

        // POST: Classes/ClassSubject
        [HttpPost]
        public IActionResult ClassSubject(SubjectsClassVM vm)
        {
            if (ModelState.IsValid)
            {
                // Calculate end time based on start time and duration
                TimeSpan duration = TimeSpan.FromHours(vm.Duration);
                vm.EndTime = vm.StartTime + duration;

                // Check if the class already has a subject assigned at the same time
                if (db.ClassesSubjects.Any(cs => cs.ClassId == vm.ClassesId &&
                                                  cs.DayOfWeek == vm.DayOfWeek &&
                                                  cs.StartTime <= vm.EndTime &&
                                                  cs.EndTime >= vm.StartTime))
                {
                    TempData["Info"] = $"Another subject is already assigned to the class at this time.";
                    return RedirectToAction("Index");
                }

                // Retrieve the class
                var classSubject = new ClassSubject
                {
                    StartTime = vm.StartTime,
                    EndTime = vm.EndTime,
                    Duration = vm.Duration,
                    DayOfWeek = vm.DayOfWeek,
                    SubjectId = vm.SubjectsId,
                    ClassId = vm.ClassesId
                };

                // Retrieve students based on age range
                //var students = db.Students.Where(s => s.Age <= vm.MaxAge && s.Age >= vm.MinAge).ToList();
                //if (students.Count == 0)
                //{
                //    TempData["Info"] = $"No student available in the specified age range.";
                //    return RedirectToAction("Index");
                //}

                // Store students' IDs in the StudentClasses entity
                //foreach (var student in students)
                //{
                //    classSubject.StudentClasses.Add(new StudentClass
                //    {
                //        StudentId = student.Id,
                //        ClassSubject = classSubject
                //    });
                //}

                db.ClassesSubjects.Add(classSubject);
                db.SaveChanges();

                //TempData["Info"] = $"ClassSubject successfully allocated to {students.Count} students.";
                return RedirectToAction("Index");
            }

            ViewBag.ClassesSubjects = db.ClassesSubjects;
            return View(vm);
        }

    }
}
