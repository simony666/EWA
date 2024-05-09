using Demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Demo.Controllers
{
    public class SubjectController : Controller
    {
        private readonly DB db;
        private readonly IWebHostEnvironment en;
        private readonly Helper hp;

        public SubjectController(DB db, IWebHostEnvironment en, Helper hp)
        {
            this.db = db;
            this.en = en;
            this.hp = hp;
        }

        // GET: Subject/Index
        public ActionResult Index()
        {
            ViewBag.Subjects = db.Subjects;
            ViewBag.Tutors = db.Tutors.ToDictionary(t => t.Id, t => t.Name);
            return View();
        }

        // POST: Subject/Create
        public ActionResult Create()
        {
            //dropdown list for room type
            ViewBag.TutorList = new SelectList(db.Tutors.OrderBy(t => t.Id), "Id", "Name");
            //ViewBag.ClassList = new SelectList(db.Classes.OrderBy(c => c.Id), "Id", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateSubjectsVM vm)
        {
            if (ModelState.IsValid)
            {
                vm.Id = NextId();
                // Calculate end time based on start time and duration
                //vm.EndTime = vm.StartTime.AddHours(vm.Duration);

                db.Subjects.Add(new()
                {
                    Id = vm.Id,
                    Name = vm.Name,
                    Fees = vm.Fees,
                    TutorId = vm.TutorId
                });
                db.SaveChanges();

                TempData["Info"] = $"Subject {vm.Id} inserted.";
                return RedirectToAction("Index");
            }

            ViewBag.Subjects = db.Subjects;
            return View(vm);
        }

        // Manually generate next id
        private string NextId()
        {
            string max = db.Subjects.Max(s => s.Id);
            if (max == null)
            {
                return "SU000"; // Return default value if no subjects exist yet
            }
            else
            {
                int n = int.Parse(max.Substring(2)); // Parse numeric part of the ID
                return "SU" + (n + 1).ToString("000"); // Increment and format the ID
            }
        }

        // GET: Subject/Update
        //[Authorize(Roles = "Member")]
        public IActionResult Update(string id)
        {
            var s = db.Subjects.Find(id);
            if (s != null)
            {
                ViewBag.TutorList = new SelectList(db.Tutors.OrderBy(t => t.Id), "Id", "Name");
                var vm = new UpdateSubjectsVM
                {
                    Id = s.Id,
                    Name = s.Name,
                    Fees = s.Fees,
                    TutorId = s.TutorId
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
        public IActionResult Update(UpdateSubjectsVM vm)
        {
            if (ModelState.IsValid)
            {
                var s = db.Subjects.Find(vm.Id);

                if (s != null)
                {
                    s.Name = vm.Name;
                    s.Fees = vm.Fees;
                    s.TutorId = vm.TutorId;
                    db.SaveChanges();
                    TempData["Info"] = "Subject updated.";
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("Id", "Subject not found.");
                }
            }
            else // If model state is invalid for POST request, reload the GET version of the page
            {
                return RedirectToAction("Update", new { id = vm.Id });
            }

            return View(vm);
        }


        // POST: Subject/Delete
        [HttpPost]
        public ActionResult Delete(string? id)
        {
            var s = db.Subjects.Find(id);

            if (s != null)
            {
                // Check if any students are associated with this class
                var studentsWithSubjects = db.ClassesSubjects.Any(s => s.SubjectsId.Contains(id));

                if (!studentsWithSubjects)
                {
                    db.Subjects.Remove(s);
                    db.SaveChanges();
                    TempData["Info"] = $"Class {s.Id} deleted.";
                }
                else
                {
                    TempData["Error"] = $"Cannot delete subject {s.Id} because there are students associated with it.";
                }
            }

            return RedirectToAction("Index");
        }
    }
}
