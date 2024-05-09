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

        // POST: Subject/Create
        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Create(ClassesVM vm)
        {
            if (ModelState.IsValid)
            {
                vm.Id = NextId();
                TempData["Info"] = $"Class {vm.Id}.";
                vm.Capacity = db.ClassesSubjects.Count(c => c.ClassesId == vm.Id);

                if (vm.Capacity > 20)
                {
                    TempData["Info"] = $"Class {vm.Id} is fully occupied.";
                    return RedirectToAction("Index");
                }

                var classes = new Classes
                {
                    Id = vm.Id,
                    Name = vm.Name,
                    Capacity = vm.Capacity.Value
                };
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


    }
}
