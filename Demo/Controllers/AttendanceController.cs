using Demo.Models;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
    public class AttendanceController : Controller
    {
        private readonly DB db;
        private readonly IWebHostEnvironment en;
        private readonly Helper hp;

        public AttendanceController(DB db, IWebHostEnvironment en, Helper hp)
        {
            this.db = db;
            this.en = en;
            this.hp = hp;
        }

        public IActionResult DB()
        {
            db.Tutors.Add(new Tutor()
            {
                Id = "T002",
                Name = "Tutor 2",
                Email = "T2@gmail.com",
                Hash = hp.HashPassword("1234"),
                Gender = "M",
                Age = 40,
                Phone = "0912345678",
            });
            db.Classes.Add(new Class
            {
                Id = "C001",
                Name = "BM",
                Capacity = 40,
            });
            db.Students.Add(new Student()
            {
                Id = "S001",
                Name = "Student",
                Gender = "M",
                Age = 40,
                Phone = "0912345678",
                ClassId = "C001",
            });
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Index()
        {
            ViewBag.Classes = db.Classes;
            return View();
        }

        public IActionResult QR()
        {
            return View();
        }
    }
}
