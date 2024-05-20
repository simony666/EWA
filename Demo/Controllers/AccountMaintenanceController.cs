using Azure.Core;
using Demo.Models;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Pdf;
using iText.Layout.Borders;
using iText.Layout.Element;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Mail;
using System.Numerics;
using System.Reflection;
using X.PagedList;

using iText.Layout;
using System.IO;
using System.Data;

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

        /*// GET: Index - Combined
        public IActionResult Index(string? id, string? sort, string? dir, int page = 1)
        {
            // (1) Searching ------------------------
            ViewBag.Name = id = id?.Trim() ?? "";

            var searched = db.Students.Where(s => s.Name.Contains(id));

            // (2) Sorting --------------------------
            ViewBag.Sort = sort;
            ViewBag.Dir = dir;

            Func<Student, object> fn = sort switch
            {
                "Id" => s => s.Id,
                "Name" => s => s.Name,
                "Gender" => s => s.Gender,
                "Role" => s => s.Role,
                _ => s => s.Id,
            };

            var sorted = dir == "des" ?
                         searched.OrderByDescending(fn) :
                         searched.OrderBy(fn);

            // (3) Paging ---------------------------
            // TODO

            if (page < 1)
            {
                return RedirectToAction(null, new { id, sort, dir, page = 1 });
            }

            var model = sorted.ToPagedList(page, 10);

            if (page > model.PageCount && model.PageCount > 0)
            {
                return RedirectToAction(null, new { id, sort, dir, page = model.PageCount });
            }

            return View(model);
        }*/

        [Authorize(Roles = "Admin")]
        // GET: Index - Combined
        public IActionResult Index(string? name, string? role, string? sort, string? dir, int page = 1)
        {

            //ViewBag.Roles = db.Users.Select(u => u.Role).Distinct().ToList();
            ViewBag.Roles = db.Users.Select(u => u.Role).Distinct();

            // (1) Searching ------------------------
            ViewBag.Name = name = name?.Trim() ?? "";

            var searched = db.Users.Where(u => u.Name.Contains(name));

            // (2) Sorting --------------------------
            ViewBag.Sort = sort;
            ViewBag.Dir = dir;

            Func<User, object> fn = sort switch
            {
                "Id" => u => u.Id,
                "Name" => u => u.Name,
                "Gender" => u => u.Gender,
                "Age" => u => u.Age,
                "Role" => u => u.Role,
                _ => u => u.Id,
            };

            var sorted = dir == "des" ?
                         searched.OrderByDescending(fn) :
                         searched.OrderBy(fn);

            // (3) Paging ---------------------------
            if (page < 1)
            {
                return RedirectToAction(null, new { name, sort, dir, page = 1 });
            }

            var model = sorted.ToPagedList(page, 10);

            if (page > model.PageCount && model.PageCount > 0)
            {
                return RedirectToAction(null, new { name, sort, dir, page = model.PageCount });
            }

            //(4) Filtering --------------------------
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_H", model); // Return a partial view for AJAX requests
            }

            return View(model);
        }

        // GET: AccountMaintenance / InsertAdmin
        [Authorize(Roles = "Admin")]
        public IActionResult InsertAdmin()
        {
            return View();
        }

        // POST: Home/InsertAdmin
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult InsertAdmin(AdminVM vm)
        {
            ValidateCommonFields(vm.Email, vm.Phone, vm.Photo);

            if (ModelState.IsValid)
            {
                string password = vm.Hash;
                Admin admin = new()
                {
                    Email = vm.Email,
                    Hash = hp.HashPassword(vm.Hash),
                    Id = AdminNextId(),
                    Name = vm.Name,
                    Age = vm.Age,
                    Gender = vm.Gender,
                    Phone = vm.Phone,
                    PhotoURL = hp.SavePhoto(vm.Photo),
                    IsActive = true
                };

                db.Admins.Add(admin);

                db.SaveChanges();

                admin.Hash = password;
                SendPasswordEmail(admin);

                TempData["Info"] = "Record Insered!";
                return RedirectToAction("Index");
            }

            TempData["Info"] = "Failed to insert record. Please check the errors and try again.";
            return View();
        }

        // GET: AccountMaintenance/ InsertTutor
        [Authorize(Roles = "Admin")]
        public IActionResult InsertTutor()
        {
            return View();
        }

        // POST: AccountMaintenance / InsertTutor
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult InsertTutor(TutorVM vm)
        {
            ValidateCommonFields(vm.Email, vm.Phone, vm.Photo);

            if (ModelState.IsValid)
            {
                string password = vm.Hash;
                Tutor tutor = new()
                {
                    Email = vm.Email,
                    Hash = hp.HashPassword(vm.Hash),
                    Id = TutorNextId(),
                    Name = vm.Name,
                    Age = vm.Age,
                    Gender = vm.Gender,
                    Phone = vm.Phone,
                    PhotoURL = hp.SavePhoto(vm.Photo),
                    IsActive = true
                };

                db.Tutors.Add(tutor);
                db.SaveChanges();

                tutor.Hash = password;
                SendPasswordEmail(tutor);

                TempData["Info"] = "Record Insered!";
                return RedirectToAction("Index");
            }

            TempData["Info"] = "Failed to insert record. Please check the errors and try again.";
            return View();
        }

        // GET: AccountMaintenance/ InsertParent
        [Authorize(Roles = "Admin")]
        public IActionResult InsertParent()
        {
            return View();
        }

        // POST: AccountMaintenance / InsertParent 
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult InsertParent(ParentVM vm)
        {
            ValidateCommonFields(vm.Email, vm.Phone, vm.Photo);

            if (ModelState.IsValid)
            {
                string password = vm.Hash;
                Parent parent = new()
                {
                    Email = vm.Email,
                    Hash = hp.HashPassword(vm.Hash),
                    Id = ParentNextId(),
                    Name = vm.Name,
                    Age = vm.Age,
                    Gender = vm.Gender,
                    Phone = vm.Phone,
                    PhotoURL = hp.SavePhoto(vm.Photo),
                    IsActive = true
                };

                db.Parents.Add(parent);
                db.SaveChanges();

                parent.Hash = password;
                SendPasswordEmail(parent);

                TempData["Info"] = "Record Insered!";
                return RedirectToAction("Index");
            }

            TempData["Info"] = "Failed to insert record. Please check the errors and try again.";
            return View();
        }


        // GET: AccountMaintenance/InsertStudent
        [Authorize(Roles = "Admin")]
        public IActionResult InsertStudent()
        {
            ViewBag.Parents = new SelectList(db.Parents, "Id", "Name");
            return View();
        }

        // POST: AccountMaintenance/InsertStudent
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult InsertStudent(StudentsVM vm)
        {
            if (ModelState.IsValid("Photo"))
            {
                var err = hp.ValidatePhoto(vm.Photo);
                if (!string.IsNullOrEmpty(err))
                {
                    ModelState.AddModelError("Photo", err);
                }
            }

            if (ModelState.IsValid)
            {
                var id = StudentNextId();

                var student = new Student
                {
                    Id = id,
                    Name = vm.Name,
                    Age = vm.Age,
                    Gender = vm.Gender,
                    PhotoURL = hp.SavePhoto(vm.Photo)
                };

                // Find the parent by ParentId (as string)
                var parent = db.Parents.FirstOrDefault(p => p.Id == vm.ParentId);
                if (parent == null)
                {
                    ModelState.AddModelError("ParentId", "Parent not found.");
                    ViewBag.Parents = new SelectList(db.Parents, "Id", "Name"); 
                    TempData["Info"] = "Failed to insert record. Please check the errors and try again.";
                    return View(vm);
                }

                if (parent.Students == null) 
                {
                    parent.Students = new List<Student>();
                }

                parent.Students.Add(student);

                db.Students.Add(student);
                db.SaveChanges();

                TempData["Info"] = "Record Inserted!";
                return RedirectToAction("Index");
            }

            ViewBag.Parents = new SelectList(db.Parents, "Id", "Name"); 
            TempData["Info"] = "Failed to insert record. Please check the errors and try again.";
            return View(vm);
        }

        //Generate Admin ID
        private string AdminNextId()
        {
            string max = db.Admins.Max(c => c.Id) ?? "A000";
            int n = int.Parse(max[1..]);
            return (n + 1).ToString("'A'000");
        }

        //generate Tutor ID
        private string TutorNextId()
        {
            string max = db.Tutors.Max(c => c.Id) ?? "T000";
            int n = int.Parse(max[1..]);
            return (n + 1).ToString("'T'000");
        }

        //generate Tutor ID
        private string ParentNextId()
        {
            string max = db.Parents.Max(c => c.Id) ?? "P000";
            int n = int.Parse(max[1..]);
            return (n + 1).ToString("'P'000");
        }

        private string StudentNextId()
        {
            string max = db.Students.Max(c => c.Id) ?? "S000";
            int n = int.Parse(max[1..]);
            return (n + 1).ToString("'S'000");
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

        //reuse the validate common fields
        private void ValidateCommonFields(string email, string phone, IFormFile photo)
        {
            if (ModelState.IsValid("Email") && db.Users.Any(u => u.Email == email))
            {
                ModelState.AddModelError("Email", "Duplicated Email.");
            }
            if (ModelState.IsValid("Phone") && db.Users.Any(u => u.Phone == phone))
            {
                ModelState.AddModelError("Phone", "Duplicated Contact No.");
            }
            if (ModelState.IsValid("Photo"))
            {
                var err = hp.ValidatePhoto(photo);
                if (!string.IsNullOrEmpty(err))
                {
                    ModelState.AddModelError("Photo", err);
                }
            }
        }


        // GET: Account / UpdateProfile
        //[Authorize(Roles = "Admin")]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateProfile(string? Id)
        {
            var m = db.Users.FirstOrDefault(a => a.Id == Id);

            var vm = new UpdateProfileByAdminVM
            {
                Email = m.Email,
                Id = m.Id,
                Name = m.Name,
                Age = m.Age,
                Gender = m.Gender,
                Phone = m.Phone,
                PhotoURL = m.PhotoURL,
                Role = m.Role
            };

            return View(vm);
        }

        // POST: Account / UpdateProfile
        //[Authorize(Roles = "Admin")]
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateProfile(UpdateProfileByAdminVM vm)
        {
            var m = db.Users.FirstOrDefault(a => a.Id == vm.Id);

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
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(string? Id)
        {
            var m = db.Users.Find(Id);

            if (m.Id == "A001")
            {
                TempData["Info"] = "This Admin Can Not Delete!!!!";
                return RedirectToAction("Index");
            }

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
            return RedirectToAction("Index");
            //return Redirect(Request.Headers.Referer.ToString());
        }

        //GET :  Detail method
        [Authorize(Roles = "Admin")]
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

        private void SendPasswordEmail(User m)
        {
            var mail = new MailMessage();
            mail.To.Add(new MailAddress(m.Email, m.Name));
            mail.Subject = "-.-. Your Eden Academy Account .-.-";
            mail.IsBodyHtml = true;

            var url = Url.Action("Login", "Account", null, "https");
            var now = DateTime.Now;

            // Generate PDF
            var pdfPath = Path.Combine(Path.GetTempPath(), $"{m.Id}_AccountDetails.pdf");
            CreatePdf(pdfPath, m, url, now);

            var att = new Attachment(pdfPath);
            mail.Attachments.Add(att);
            att.ContentId = "accountDetails";

            mail.Body = $@"
            <p>Dear {m.Name},</p>
            <p>Your Eden Academy Account has been created. Please find the account details attached as a PDF document.</p>
            <p>
                Please <a href='{url}'>login</a>
                with your email and password.
            </p>
            <p>From, 🐱 Eden Academy Admin</p>
            ";

            hp.SendEmail(mail);
        }

        private void CreatePdf(string path, User user, string loginUrl, DateTime creationTime)
        {
            using (var writer = new PdfWriter(path))
            {
                using (var pdf = new PdfDocument(writer))
                {
                    var document = new Document(pdf);

                    // Add image
                    var imagePath = Path.Combine(en.WebRootPath, "photos", user.PhotoURL);
                    if (System.IO.File.Exists(imagePath))
                    {
                        var imageData = ImageDataFactory.Create(imagePath);
                        var img = new iText.Layout.Element.Image(imageData);
                        img.SetWidth(200).SetHeight(200).SetBorder(new SolidBorder(1));
                        document.Add(img);
                    }

                    // Add user details
                    document.Add(new Paragraph($"Dear {user.Name},"));
                    document.Add(new Paragraph($"Email: {user.Email}").SetFontColor(ColorConstants.RED));
                    document.Add(new Paragraph($"Password: {user.Hash}").SetFontColor(ColorConstants.RED));
                    document.Add(new Paragraph($"Account created on: {creationTime.ToString("f")}"));
                    document.Add(new Paragraph("From, 🐱 Eden Academy Admin"));
                }
            }
        }
    }
}