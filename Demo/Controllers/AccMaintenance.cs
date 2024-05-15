using Demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Demo.Controllers
{
    public class AccMaintenance : Controller
    {
        private readonly DB db;
        private readonly IWebHostEnvironment en;
        private readonly Helper hp;

        public AccMaintenance(DB db, IWebHostEnvironment en, Helper hp)
        {
            this.db = db;
            this.en = en;
            this.hp = hp;
        }

        // GET: Home/Insert
        public IActionResult Insert()
        {
            ViewBag.ProgramList = new SelectList(db.Programs, "Id", "Name");
            return View();
        }

        // POST: Home/Insert
        [HttpPost]
        [RequestSizeLimit(100 * 1024 * 1024)] // 100MB
        [RequestFormLimits(MultipartBodyLengthLimit = 100 * 1024 * 1024)] // 100MB
        public IActionResult Insert(AdminVM vm, IFormFile photo)
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
            if (ModelState.IsValid("contactNo") &&
                db.Admins.Any(a => a.ContactNo == vm.ContactNo))
            {
                ModelState.AddModelError("ContactNo", "Duplicated Contact No.");
            }
            if (ModelState.IsValid("photo"))
            {
                // TODO
                var e = hp.ValidatePhoto(photo);
                if (e != "") ModelState.AddModelError("photo", e);
            }

            if (ModelState.IsValid)
            {
                hp.SavePhoto(photo);

                db.Admins.Add(new()
                {
                    Email = vm.Email,
                    Hash = vm.Hash,
                    Id = vm.Id,
                    Name = vm.Name,
                    Gender = vm.Gender,
                    ContactNo = vm.ContactNo,
                    PhotoURL = photo,
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


    }
}
