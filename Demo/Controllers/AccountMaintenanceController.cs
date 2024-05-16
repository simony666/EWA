using Azure.Core;
using Demo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Numerics;
using System.Reflection;

namespace Demo.Controllers
{
    public class AccountMaintenanceController : Controller
    {
        private readonly DB db;
        private readonly IWebHostEnvironment en;
        private readonly Helper hp;

        public AccountMaintenanceController(DB db, IWebHostEnvironment en, Helper hp)
        {
            this.db = db;
            this.en = en;
            this.hp = hp;
        }

        //AccountMaintenance / Index
        public IActionResult Index()
        {
            var users = db.Users.Select(user => new UserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                Gender = user.Gender,
                Phone = user.Phone,
                Age = user.Age,
                Role = user.GetType().Name
            }).ToList();

            return View(users);
        }

        // GET: AccountMaintenance / InsertAdmin
        public IActionResult InsertAdmin()
        {
            return View();
        }

        // POST: Home/InsertAdmin
        [HttpPost]
        public IActionResult InsertAdmin(AdminVM vm)
        {
            //vm.Id = AdminNextId();
            if (ModelState.IsValid("Email") &&
                db.Admins.Any(a => a.Email == vm.Email))
            {
                ModelState.AddModelError("Email", "Duplicated Email.");
            }
            /*if (ModelState.IsValid("Id") &&
                db.Admins.Any(a => a.Id == vm.Id))
            {
                ModelState.AddModelError("Id", "Duplicated Id.");
            }*/
            if (ModelState.IsValid("Phone") &&
                db.Admins.Any(a => a.Phone == vm.Phone))
            {
                ModelState.AddModelError("Phone", "Duplicated Contact No.");
            }
            if (ModelState.IsValid("Photo"))
            {
                var err = hp.ValidatePhoto(vm.Photo);
                if (err != "") ModelState.AddModelError("Photo", err);
            }

            if (ModelState.IsValid)
            {
                db.Admins.Add(new()
                {
                    Email = vm.Email,
                    Hash = vm.Hash,
                    //Id = vm.Id,
                    Id = AdminNextId(),
                    Name = vm.Name,
                    Age = vm.Age,
                    Gender = vm.Gender,
                    Phone = vm.Phone,
                    PhotoURL = hp.SavePhoto(vm.Photo)
                }) ;


                db.SaveChanges();

                TempData["Info"] = "Record Insered!";
                return RedirectToAction("Index");
            }

            TempData["Info"] = "Failed to insert record. Please check the errors and try again.";
            return View();
        }

        // GET: AccountMaintenance/ InsertTutor
        public IActionResult InsertTutor()
        {
            return View();
        }

        // POST: AccountMaintenance / InsertTutor
        [HttpPost]
        public IActionResult InsertTutor(TutorVM vm)
        {
            //vm.Id = AdminNextId();
            if (ModelState.IsValid("Email") &&
                db.Admins.Any(a => a.Email == vm.Email))
            {
                ModelState.AddModelError("Email", "Duplicated Email.");
            }
            /*if (ModelState.IsValid("Id") &&
                db.Admins.Any(a => a.Id == vm.Id))
            {
                ModelState.AddModelError("Id", "Duplicated Id.");
            }*/
            if (ModelState.IsValid("Phone") &&
                db.Admins.Any(a => a.Phone == vm.Phone))
            {
                ModelState.AddModelError("Phone", "Duplicated Contact No.");
            }
            if (ModelState.IsValid("Photo"))
            {
                var err = hp.ValidatePhoto(vm.Photo);
                if (err != "") ModelState.AddModelError("Photo", err);
            }

            if (ModelState.IsValid)
            {
                db.Tutors.Add(new()
                {
                    Email = vm.Email,
                    Hash = vm.Hash,
                    //Id = vm.Id,
                    Id = AdminNextId(),
                    Name = vm.Name,
                    Age = vm.Age,
                    Gender = vm.Gender,
                    Phone = vm.Phone,
                    PhotoURL = hp.SavePhoto(vm.Photo)
                });


                db.SaveChanges();

                TempData["Info"] = "Record Insered!";
                return RedirectToAction("Index");
            }

            TempData["Info"] = "Failed to insert record. Please check the errors and try again.";
            return View();
        }

        //Generate Admin ID
        private string AdminNextId()
        {
            string max = db.Admins.Max(c => c.Id) ?? "A00000";
            int n = int.Parse(max[1..]);
            return (n + 1).ToString("'A'00000");
        }

        //generate Tutor ID
        private string TutorNextId()
        {
            string max = db.Tutors.Max(c => c.Id) ?? "T00000";
            int n = int.Parse(max[1..]);
            return (n + 1).ToString("'T'00000");
        }


        // GET: AccountMaintenance / CheckId 
        public bool CheckId(string id)
        {
            return !db.Users.Any(s => id == s.Id);
        }

        // GET: AccountMaintenance / CheckEmail
        public bool CheckEmail(string email)
        {
            return !db.Users.Any(s => email == s.Email);
        }

        // GET: AccountMaintenance / CheckPhone
        public bool CheckPhone(string phone)
        {
            return !db.Users.Any(s => phone == s.Phone);
        }


        // GET: Account / UpdateProfile
        //[Authorize(Roles = "Admin")]
        public IActionResult UpdateProfile(string? Id)
        {
            var m = db.Users.FirstOrDefault(a => a.Id == Id);
            //if (m == null) return RedirectToAction("Index", "Home");

            var vm = new UpdateProfileByAdminVM
            {
                Email = m.Email,
                /*Hash = m.Hash,*/
                Id = m.Id,
                Name = m.Name,
                Age = m.Age,
                Gender = m.Gender,
                Phone = m.Phone,
                PhotoURL = m.PhotoURL
            };

            return View(vm);
        }

        // POST: Account / UpdateProfile
        //[Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult UpdateProfile(UpdateProfileByAdminVM vm)
        {
            var m = db.Users.FirstOrDefault(a => a.Id == vm.Id);
            //if (m == null) return RedirectToAction("Index", "Home");

            if (vm.Photo != null)
            {
                var err = hp.ValidatePhoto(vm.Photo);
                if (err != "") ModelState.AddModelError("Photo", err);
            }

            if (ModelState.IsValid)
            {
                m.Name = vm.Name;
                m.Age = vm.Age;
                m.Gender = vm.Gender;
                m.Phone = vm.Phone;

                if (vm.Photo != null)
                {
                    hp.DeletePhoto(m.PhotoURL);
                    m.PhotoURL = hp.SavePhoto(vm.Photo);
                    HttpContext.Session.SetString("PhotoURL", m.PhotoURL);
                }

                db.SaveChanges();

                TempData["Info"] = "Profile updated.";
                return RedirectToAction("Index");
            }

            vm.Email = m.Email;
            vm.Age = m.Age;
            vm.Gender = m.Gender;
            vm.PhotoURL = m.PhotoURL;
            return Redirect(Request.Headers.Referer.ToString());
        }

        //POST : AccountMaintenance / Delete
        [HttpPost]
        public IActionResult Delete(string? Id)
        {
            var m = db.Users.Find(Id);

            if (m != null)
            {
                hp.DeletePhoto(m.PhotoURL);
                db.Users.Remove(m);
                db.SaveChanges();

                if (!Request.IsAjax())
                {
                    TempData["Info"] = "Record Deleted";
                }
            }
            //return RedirectToAction("Index");
            return Redirect(Request.Headers.Referer.ToString());
        }

        //GET :  Detail method
        public IActionResult Detail(string? id)
        {
            var u = db.Users.FirstOrDefault(h => h.Id == id);
            if (u == null)
            {
                return NotFound();
            }

            var UserViewModel = new UserViewModel
            {
                Id = u.Id,
                Email = u.Email,
                Name = u.Name,
                Gender = u.Gender,
                Phone = u.Phone,
                Age = u.Age,
                Role = u.Role,
                PhotoURL = u.PhotoURL
            };

            return View(UserViewModel);
        }
    }
}