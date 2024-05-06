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
            return View();
        }

        // POST: Subject/Create
        public ActionResult Create()
        {
            //dropdown list for room type
            ViewBag.TutorList = new SelectList(db.Tutors.OrderBy(t => t.Id), "Id", "Name");
            ViewBag.ClassesList = new SelectList(db.Classes.OrderBy(c => c.Id), "Id", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateSubjectsVM vm)
        {
            if (ModelState.IsValid)
            {
                vm.Id = NextId();
                // Calculate end time based on start time and duration
                vm.EndTime = vm.StartTime.AddHours(vm.Duration);

                db.Subjects.Add(new()
                {
                    Id = vm.Id,
                    Name = vm.Name,
                    Fees = vm.Fees,
                    DayOfWeek = vm.DayOfWeek,
                    StartTime = vm.StartTime,
                    Duration = vm.Duration,
                    EndTime = vm.EndTime,
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
            string max = db.Subjects.Max(s => s.Id) ?? "S000";
            int n = int.Parse(max[1..]);
            return (n + 1).ToString("'SU'000");
        }

        // POST: Subject/Delete
        [HttpPost]
        public ActionResult Delete(string? id)
        {
            var s = db.Subjects.Find(id);

            if (s != null)
            {
                db.Subjects.Remove(s);
                db.SaveChanges();
                TempData["Info"] = $"Subject {s.Id} deleted.";
            }

            return RedirectToAction("Index");
        }
    }
}
