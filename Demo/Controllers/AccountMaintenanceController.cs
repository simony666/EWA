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

        // GET: Index - Combined
        public IActionResult Index(string? name, string? sort, string? dir, int page = 1)
        {
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

            return View(model);
        }

        /*//AccountMaintenance / Index
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
        }*/

        // GET: AccountMaintenance / InsertAdmin
        public IActionResult InsertAdmin()
        {
            return View();
        }

        // POST: Home/InsertAdmin
        [HttpPost]
        public IActionResult InsertAdmin(AdminVM vm)
        {
            /*if (ModelState.IsValid("Email") &&
                db.Admins.Any(a => a.Email == vm.Email))
            {
                ModelState.AddModelError("Email", "Duplicated Email.");
            }
            if (ModelState.IsValid("Phone") &&
                db.Admins.Any(a => a.Phone == vm.Phone))
            {
                ModelState.AddModelError("Phone", "Duplicated Contact No.");
            }
            if (ModelState.IsValid("Photo"))
            {
                var err = hp.ValidatePhoto(vm.Photo);
                if (err != "") ModelState.AddModelError("Photo", err);
            }*/

            ValidateCommonFields(vm.Email, vm.Phone, vm.Photo);

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
                });

                db.SaveChanges();

                var m = db.Users.FirstOrDefault(a => a.Email == vm.Email);
                SendPasswordEmail(m);
                //SendPasswordEmail(vm.Email, vm.Hash, vm.Name);


                //db.SaveChanges();

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
            /*if (ModelState.IsValid("Email") &&
                db.Admins.Any(a => a.Email == vm.Email))
            {
                ModelState.AddModelError("Email", "Duplicated Email.");
            }
            if (ModelState.IsValid("Phone") &&
                db.Admins.Any(a => a.Phone == vm.Phone))
            {
                ModelState.AddModelError("Phone", "Duplicated Contact No.");
            }
            if (ModelState.IsValid("Photo"))
            {
                var err = hp.ValidatePhoto(vm.Photo);
                if (err != "") ModelState.AddModelError("Photo", err);
            }*/

            ValidateCommonFields(vm.Email, vm.Phone, vm.Photo);

            if (ModelState.IsValid)
            {
                db.Tutors.Add(new()
                {
                    Email = vm.Email,
                    Hash = vm.Hash,
                    //Id = vm.Id,
                    Id = TutorNextId(),
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

        // GET: AccountMaintenance/ InsertParent
        public IActionResult InsertParent()
        {
            return View();
        }

        // POST: AccountMaintenance / InsertParent 
        [HttpPost]
        public IActionResult InsertParent(ParentVM vm)
        {
            /*if (ModelState.IsValid("Email") &&
                db.Admins.Any(a => a.Email == vm.Email))
            {
                ModelState.AddModelError("Email", "Duplicated Email.");
            }
            if (ModelState.IsValid("Phone") &&
                db.Admins.Any(a => a.Phone == vm.Phone))
            {
                ModelState.AddModelError("Phone", "Duplicated Contact No.");
            }
            if (ModelState.IsValid("Photo"))
            {
                var err = hp.ValidatePhoto(vm.Photo);
                if (err != "") ModelState.AddModelError("Photo", err);
            }*/

            ValidateCommonFields(vm.Email, vm.Phone, vm.Photo);

            if (ModelState.IsValid)
            {
                db.Parents.Add(new()
                {
                    Email = vm.Email,
                    Hash = vm.Hash,
                    //Id = vm.Id,
                    Id = ParentNextId(),
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


        // GET: AccountMaintenance/InsertStudent
        public IActionResult InsertStudent()
        {
            ViewBag.Parents = new SelectList(db.Parents, "Id", "Name");
            return View();
        }

        // POST: AccountMaintenance/InsertStudent
        [HttpPost]
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
                    ModelState.AddModelError("ParentId", "Parent not found."); // Use ParentId for error
                    ViewBag.Parents = new SelectList(db.Parents, "Id", "Name"); // Populate the dropdown again
                    TempData["Info"] = "Failed to insert record. Please check the errors and try again.";
                    return View(vm);
                }

                if (parent.Students == null) // Ensure the Students collection is initialized
                {
                    parent.Students = new List<Student>();
                }

                // Add student to parent's Students list
                parent.Students.Add(student);

                db.Students.Add(student); // Add student to Students table
                db.SaveChanges(); // Save changes

                TempData["Info"] = "Record Inserted!";
                return RedirectToAction("Index");
            }

            ViewBag.Parents = new SelectList(db.Parents, "Id", "Name"); // Populate the dropdown again
            TempData["Info"] = "Failed to insert record. Please check the errors and try again.";
            return View(vm);
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

        //generate Tutor ID
        private string ParentNextId()
        {
            string max = db.Parents.Max(c => c.Id) ?? "P00000";
            int n = int.Parse(max[1..]);
            return (n + 1).ToString("'P'00000");
        }

        private string StudentNextId()
        {
            string max = db.Students.Max(c => c.Id) ?? "S00000";
            int n = int.Parse(max[1..]);
            return (n + 1).ToString("'S'00000");
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
                PhotoURL = m.PhotoURL,
                Role = m.Role
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
            return RedirectToAction("Index");
            //return Redirect(Request.Headers.Referer.ToString());
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

        /*private void SendPasswordEmail(User m)
        {
            var mail = new MailMessage();
            mail.To.Add(new MailAddress(m.Email, m.Name));
            mail.Subject = "-.-.  Your Eden Academy Account  .-.-";
            mail.IsBodyHtml = true;

            var now = DateTime.Now;

            var url = Url.Action("Login", "Account", null, "https");



            var path = m switch
            {
                //Admin => Path.Combine(en.WebRootPath, "images", "admin.png"),
                User u => Path.Combine(en.WebRootPath, "photos", u.PhotoURL),
                _ => ""
            };

            var att = new Attachment(path);
            mail.Attachments.Add(att);
            att.ContentId = "photo";

            mail.Body = $@"
            <img src='cid:photo' style='width: 200px; height: 200px; border: 1px solid #333'>
            <p>Dear {m.Name},<p>
            <p>Your Eden Academy Account had been created:</p>
            <h1 style='color: red'>{m.Email}</h1>
            <h1 style='color: red'>{m.Hash}</h1>
            <p>
                Please <a href='{url}'>login</a>
                with your email and password.
            </p>
            <p> 
                Your account was created on {now.ToString("f")}.
            </p>
            <p>From, 🐱 Eden Academy Admin</p>
        ";

            hp.SendEmail(mail);
        }*/

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

            // Clean up the temporary file
            /*if (System.IO.File.Exists(pdfPath))
            {
                System.IO.File.Delete(pdfPath);
            }*/
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
                    document.Add(new Paragraph("Your Eden Academy Account has been created:"));
                    document.Add(new Paragraph($"Email: {user.Email}").SetFontColor(ColorConstants.RED));
                    document.Add(new Paragraph($"Password: {user.Hash}").SetFontColor(ColorConstants.RED));
                    document.Add(new Paragraph($"Account created on: {creationTime.ToString("f")}"));
                    //document.Add(new Paragraph($@"Please login with your email and password: {loginUrl}"));

                    document.Add(new Paragraph("From, 🐱 Eden Academy Admin"));
                }
            }
        }
    }
}