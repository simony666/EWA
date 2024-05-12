using Demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Demo.Controllers
{
    public class AccMaintenance : Controller
    {
        private readonly DB db;

        public AccMaintenance(DB db)
        {
            this.db = db;
        }

        // GET: Home/Insert
        public IActionResult Insert()
        {
            ViewBag.ProgramList = new SelectList(db.Programs, "Id", "Name");
            return View();
        }

        // POST: Home/Insert
        [HttpPost]
        public IActionResult Insert(AdminVM vm, InsertVm am)
        {
            if (ModelState.IsValid("email") &&
                db.Admins.Any(a => a.Email == vm.Email))
            {
                ModelState.AddModelError("Email", "Duplicated Email.");
            }
            if (ModelState.IsValid("id") &&
                db.Admins.Any(a => a.Id == vm.Id))
            {
                ModelState.AddModelError("Id", "Duplicated Id.");
            }
            if (ModelState.IsValid("contactNo")&&
                db.Admins.Any(a => a.ContactNo ==  vm.ContactNo))
            {
                ModelState.AddModelError("ContactNo", "Duplicated Contact No.");
            }

            if (ModelState.IsValid)
            {
                db.Admins.Add(new()
                {
                    Email = vm.Email,
                    Hash = vm.Hash,
                    Id = vm.Id,
                    Name = vm.Name,
                    Gender = vm.Gender,
                    ContactNo = vm.ContactNo,
                });
                db.SaveChanges();

                TempData["Info"] = "Record Insered!";
                return RedirectToAction("Index");
            }

            ViewBag.ProgramList = new SelectList(db.Programs, "Id", "Name");
            return View();
        }


        // GET: Home/CheckId 
        public bool CheckId(string id)
        {
            return !db.Students.Any(s => id == s.Id);
        }


        // GET: Home/CheckProgramId
        public bool CheckProgramId(string programId)
        {
            return db.Programs.Any(p => p.Id == programId);
        }

    }
}
