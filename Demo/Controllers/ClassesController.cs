/*using Demo.Models;
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
            ViewBag.Classes = db.Classes;
            return View();
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
                //vm.ClassType = AssignType();
                //if(vm.ClassType == "null")
                //{
                //    TempData["Info"] = "No students is under 3-6 years old";
                //    return RedirectToAction("Index");
                //}

                // Create a new class object
                var classes = new Classes
                {
                    Id = vm.Id,
                    Name = vm.Name,
                    ClassType = vm.ClassType
                };

                // Count the number of students enrolled in the class
                classes.Capacity = db.ClassesSubjects.Count(c => c.ClassesId == classes.Id);

                // Check if the capacity exceeds the limit
                if (classes.Capacity > 20)
                {
                    TempData["Info"] = $"Class {vm.Id} is fully occupied.";
                    return RedirectToAction("Index");
                }

                // Add the class object to the context and save changes
                db.Classes.Add(classes);
                db.SaveChanges();

                TempData["Info"] = $"Class {vm.Id} inserted."; // Set Success message in TempData

                return RedirectToAction("Index");
            }

            ViewBag.Classes = db.Classes;
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
                    db.SaveChanges();
                    TempData["Info"] = "Class updated.";
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
                var studentsWithClass = db.ClassesSubjects.Any(s => s.ClassesId.Contains(id));

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

        // GET: Classes/TutorClass
        public IActionResult TutorClass(string? id)
        {
            var t = db.Tutors.Find(id);

            if (t != null)
            {
                var classesWithoutTutor = db.Classes.Where(c => !db.ClassesSubjects.Any(cs => cs.ClassesId == c.Id))
                                                    .OrderBy(c => c.Id)
                                                    .ToList();
                ViewBag.ClassesList = new SelectList(classesWithoutTutor, "Id", "Name");

                var vm = new TutorClassVM
                {
                    Id = t.Id,
                    Name = t.Name
                };

                return View(vm);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // POST: Classes/TutorClass
        [HttpPost]
        public IActionResult TutorClass(TutorClassVM vm)
        {
            if (ModelState.IsValid)
            {
                var t = db.Tutors.Find(vm.Id);
                if (t != null)
                {
                    t.Id = vm.Id;
                    t.ClassId = vm.ClassId;
                    db.SaveChanges();
                    TempData["Info"] = "Class updated.";
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(vm);
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
                if (db.ClassesSubjects.Any(cs => cs.ClassesId == vm.ClassesId &&
                                                  cs.DayOfWeek == vm.DayOfWeek &&
                                                  cs.StartTime <= vm.EndTime &&
                                                  cs.EndTime >= vm.StartTime))
                {
                    TempData["Info"] = $"Another subject is already assigned to the class at this time.";
                    return RedirectToAction("Index");
                }

                // Retrieve the class
                var classSubject = new ClassesSubjects
                {
                    StartTime = vm.StartTime,
                    EndTime = vm.EndTime,
                    Duration = vm.Duration,
                    DayOfWeek = vm.DayOfWeek,
                    SubjectsId = vm.SubjectsId,
                    ClassesId = vm.ClassesId
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

        // GET: /Classes/StudentClass
        public IActionResult StudentClass(string id)
        {
            var classes = db.Classes.Include(s => s.Students)
                                    .FirstOrDefault(c => c.Id == id);

            if (classes == null)
            {
                return RedirectToAction("index");
            }

            // each of the subject display the selected id
            var selected = classes.Students.Select(s => s.Id);

            // Id as value hidden inside the checkbox, Name for display 
            ViewBag.StudentList = new MultiSelectList(db.Students, "Id", "Name", selected);
            return View(classes);
        }

        [HttpPost]
        public IActionResult StudentClass(string id, string[] students)
        {
            var classes = db.Classes.FirstOrDefault(c => c.Id == id);

            if (classes == null)
            {
                return RedirectToAction("Index");
            }

            // Update the ClassesId for selected students with null ClassesId
            foreach (var studentId in students)
            {
                var student = db.Students.FirstOrDefault(s => s.Id == studentId);

                if (student != null)
                {
                    // Update ClassesId only if it's null
                    if (student.ClassesId == null)
                    {
                        student.ClassesId = classes.Id;
                    }
                }
                else
                {
                    // Student not found
                    TempData["Info"] = $"Student {studentId} not found.";
                    return RedirectToAction("Index");
                }
            }

            // Save changes outside of loop
            db.SaveChanges();

            TempData["Info"] = "Student(s) assigned.";
            return RedirectToAction("Index");
        }



        // assign classType for classes based on student's age
        private string AssignType(int age)
        {
            if (age == 3 || age == 4)
            {
                return "K1";
            }
            else if (age == 5)
            {
                return "K2";
            }
            else if (age == 6)
            {
                return "K3";
            }
            else
            {
                return null;
            }
        }

    }
}
*/