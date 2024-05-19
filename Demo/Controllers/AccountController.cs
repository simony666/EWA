using Demo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using Newtonsoft.Json.Linq;

namespace Demo.Controllers;

public class AccountController : Controller
{
    private readonly DB db;
    private readonly IWebHostEnvironment en;
    private readonly Helper hp;
    

    public AccountController(DB db, IWebHostEnvironment en, Helper hp)
    {
        this.db = db;
        this.en = en;
        this.hp = hp;        
    }

    public IActionResult DB()
    {
        db.Tutors.Add(new Tutor()
        {
            Id = "T004",
            Name = "Tutor 3",
            Email = "wangsy-wm22@student.tarc.edu.my",
            Hash = hp.HashPassword("12345"),
            Gender = "M",
            Age = 40,
            PhotoURL = "xxx.jpg"
            
        });
        db.SaveChanges();
        return RedirectToAction("Index");
    }

    // GET: Account/Login
    public IActionResult Login()
    {
        ViewBag.CaptchaImage = "";
        return View();
    }

    // POST: Account/Login
    [HttpPost]
    public IActionResult Login(LoginVM vm, string? returnURL)
    {
        var recaptchaSecretKey = "6LedfeEpAAAAALifWprF-8ls_34CvzEQQTfqnocx";
        var recaptchaResponse = vm.RecaptchaResponse;
        var httpClient = new HttpClient();
        var response = httpClient.GetAsync($"https://www.google.com/recaptcha/api/siteverify?secret={recaptchaSecretKey}&response={recaptchaResponse}").Result;
        var recaptchaResult = response.Content.ReadAsStringAsync().Result;
        var recaptchaObj = JObject.Parse(recaptchaResult);
        var recaptchaSuccess = (bool)recaptchaObj.SelectToken("success");
        if (!recaptchaSuccess)
        {
            ModelState.AddModelError("", "reCAPTCHA verification failed.");
        }

        var u = db.Users.FirstOrDefault(u => u.Email == vm.Email);

        if (u == null || !hp.VerifyPassword(u.Hash, vm.Password))
        {
            ModelState.AddModelError("", "Login credentials not matched.");
        }

        if (u != null && !u.IsActive)
        {
            ModelState.AddModelError("", "Account is not activated. Please check your email and activate your account.");
        }

        if (ModelState.IsValid)
        {
            TempData["Info"] = "Login successfully.";

            hp.SignIn(u!.Email, u.Role, vm.RememberMe);

            if (u is Parent p)
            {
                HttpContext.Session.SetString("PhotoURL", p.PhotoURL);
            }

            if (string.IsNullOrEmpty(returnURL))
            {
                return RedirectToAction("Index", "Home");
            }

            return Redirect(returnURL);
        }

        ViewBag.CaptchaImage = "";
        return View(vm);
    }

    // GET: Account/Logout
    public IActionResult Logout(string? returnURL)
    {
        TempData["Info"] = "Logout successfully.";

        hp.SignOut();

        HttpContext.Session.Clear();

        return RedirectToAction("Index", "Home");
    }

    // GET: Account/AccessDenied
    public IActionResult AccessDenied(string? returnURL)
    {
        return View();
    }



    // ------------------------------------------------------------------------
    // Others
    // ------------------------------------------------------------------------

    // GET: Account/CheckEmail
    public bool CheckEmail(string email)
    {
        return !db.Users.Any(u => u.Email == email);
    }

    // GET: Account/Register
    public IActionResult Register()
    {
        return View();
    }

    // POST: Account/Register
    [HttpPost]
    public IActionResult Register(RegisterVM vm)
    {
        if (ModelState.IsValid("Email") && db.Users.Any(u => u.Email == vm.Email))
        {
            ModelState.AddModelError("Email", "Duplicated Email.");
        }

        if (ModelState.IsValid("Photo"))
        {
            var err = hp.ValidatePhoto(vm.Photo);
            if (err != "") ModelState.AddModelError("Photo", err);
        }

        if (ModelState.IsValid)
        {
            var newParent = new Parent
            {
                Id = NextId(),
                Email = vm.Email,
                Hash = hp.HashPassword(vm.Password),
                Name = vm.Name,
                Gender = vm.Gender,
                Phone = vm.Phone,
                PhotoURL = hp.SavePhoto(vm.Photo),
                Age = vm.Age, // Set Age property
                IsActive = false
            };

            db.Users.Add(newParent);
            db.SaveChanges();

            var token = new ActiveToken
            {
                UserId = newParent.Id,
                Token = GenerateActivationToken(),
                Expire = DateTime.UtcNow.AddHours(3)
            };
            db.ActiveTokens.Add(token);
            db.SaveChanges();

            SendActivationEmail(newParent, token.Token);

            TempData["Info"] = "Register successfully. Please check your email to activate your account.";
            return RedirectToAction("Login");
        }

        return View(vm);
    }

    // Method to generate activation token
    private string GenerateActivationToken()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var random = new Random();
        return new string(Enumerable.Repeat(chars, 6)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    // Method to send activation email
    private void SendActivationEmail(User u, string token)
    {
        var mail = new MailMessage();
        mail.To.Add(new MailAddress(u.Email, u.Name));
        mail.Subject = "Account Activation";
        mail.IsBodyHtml = true;

        var activationLink = Url.Action("Activate", "Account", new { token = token }, Request.Scheme);

        var path = u switch
        {
            Admin => Path.Combine(en.WebRootPath, "images", "admin.png"),
            User p => Path.Combine(en.WebRootPath, "photos", p.PhotoURL),
            _ => ""
        };

        var att = new Attachment(path);
        mail.Attachments.Add(att);
        att.ContentId = "photo";

        mail.Body = $@"
            <img src='cid:photo' style='width: 200px; height: 200px; border: 1px solid #333'>
            <p>Dear {u.Name},</p>
            <p>Please activate your account by clicking the link below:</p>
            <p><a href='{activationLink}'>Activate Account</a></p>
            <p>Thank you!</p>
        ";

        hp.SendEmail(mail);
    }

    // Manually generate next id
    private string NextId()
    {
        string max = db.Parents.Max(p => p.Id) ?? "P000";
        int n = int.Parse(max[1..]);
        return (n + 1).ToString("'P'000");
    }

    // Activation action
    public IActionResult Activate(string token)
    {
        var activeToken = db.ActiveTokens.SingleOrDefault(t => t.Token == token && t.Expire > DateTime.UtcNow);
        if (activeToken != null)
        {
            var user = db.Parents.SingleOrDefault(u => u.Id == activeToken.UserId);
            if (user != null)
            {
                user.IsActive = true; // Assuming you have an IsActive property
                db.ActiveTokens.Remove(activeToken); // Remove the token after activation
                db.SaveChanges();
                TempData["Info"] = "Account activated successfully. Please login.";
                return RedirectToAction("Login");
            }
        }

        TempData["Error"] = "Invalid or expired activation token.";
        return RedirectToAction("Register");
    }

    // GET: Account/UpdateProfile
    [Authorize(Roles = "Parent")]
    public IActionResult UpdateProfile()
    {
        var m = db.Users.FirstOrDefault(u => u.Email == User.Identity!.Name);
        if (m == null) return RedirectToAction("Index", "Home");

        var vm = new UpdateProfileVM
        {
            Email = m.Email,
            Name = m.Name,
            PhotoURL = m.PhotoURL
        };

        return View(vm);
    }

    // POST: Account/UpdateProfile
    [Authorize(Roles = "Parent")]
    [HttpPost]
    public IActionResult UpdateProfile(UpdateProfileVM vm)
    {
        var m = db.Users.FirstOrDefault(u => u.Email == User.Identity!.Name);
        if (m == null) return RedirectToAction("Index", "Home");

        if (vm.Photo != null)
        {
            var err = hp.ValidatePhoto(vm.Photo);
            if (err != "") ModelState.AddModelError("Photo", err);
        }

        if (ModelState.IsValid)
        {
            m.Name = vm.Name;

            if (vm.Photo != null)
            {
                hp.DeletePhoto(m.PhotoURL);
                m.PhotoURL = hp.SavePhoto(vm.Photo);
                HttpContext.Session.SetString("PhotoURL", m.PhotoURL);
            }

            db.SaveChanges();

            TempData["Info"] = "Profile updated.";
            return RedirectToAction();
        }

        vm.Email = m.Email;
        vm.PhotoURL = m.PhotoURL;
        return View(vm);
    }

    // GET: Account/UpdatePassword
    [Authorize]
    public IActionResult UpdatePassword()
    {
        return View();
    }

    // POST: Account/UpdatePassword
    [Authorize]
    [HttpPost]
    public IActionResult UpdatePassword(UpdatePasswordVM vm)
    {
        var u = db.Users.FirstOrDefault(u => u.Email == User.Identity!.Name);
        if (u == null) return RedirectToAction("Index", "Home");

        if (!hp.VerifyPassword(u.Hash, vm.Current))
        {
            ModelState.AddModelError("Current", "Current Password not matched.");
        }

        if (ModelState.IsValid)
        {
            u.Hash = hp.HashPassword(vm.New);
            db.SaveChanges();

            TempData["Info"] = "Password updated.";
            return RedirectToAction();
        }

        return View();
    }

    // GET: Account/ResetPassword
    public IActionResult ResetPassword()
    {
        return View();
    }

    // POST: Account/ResetPassword
    [HttpPost]
    public IActionResult ResetPassword(ResetPasswordVM vm)
    {
        var u = db.Users.FirstOrDefault(u => u.Email == vm.Email);

        if (u == null)
        {
            ModelState.AddModelError("Email", "Email not found.");
        }

        if (ModelState.IsValid)
        {
            string password = hp.RandomPassword();

            u!.Hash = hp.HashPassword(password);
            db.SaveChanges();

            SendResetPasswordEmail(u, password);

            TempData["Info"] = "Password reset. Check your email.";
            return RedirectToAction();
        }

        return View();
    }

    private void SendResetPasswordEmail(User u, string password)
    {
        var mail = new MailMessage();
        mail.To.Add(new MailAddress(u.Email, u.Name));
        mail.Subject = "Reset Password";
        mail.IsBodyHtml = true;

        var url = Url.Action("Login", "Account", null, "https");

        var path = u switch
        {
            Admin => Path.Combine(en.WebRootPath, "images", "admin.png"),
            User p => Path.Combine(en.WebRootPath, "photos", p.PhotoURL),
            _ => ""
        };

        var att = new Attachment(path);
        mail.Attachments.Add(att);
        att.ContentId = "photo";

        mail.Body = $@"
            <img src='cid:photo' style='width: 200px; height: 200px; border: 1px solid #333'>
            <p>Dear {u.Name},<p>
            <p>Your password has been reset to:</p>
            <h1 style='color: red'>{password}</h1>
            <p>
                Please <a href='{url}'>login</a>
                with your new password.
            </p>
            <p>From, 🐱 Super Admin</p>
        ";

        hp.SendEmail(mail);
    }
}



