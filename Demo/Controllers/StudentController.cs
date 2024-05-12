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
    public class StudentController : Controller
    {
        private readonly DB db;
        private readonly IWebHostEnvironment en;
        private readonly Helper hp;

        public StudentController(DB db, IWebHostEnvironment en, Helper hp)
        {
            this.db = db;
            this.en = en;
            this.hp = hp;
        }

        public IActionResult Index()
        {
            ViewBag.studentList = db.Students;
            return View();
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            return View();
        }

        // GET: Student/AssignClass
        public IActionResult AssignClass(string? id)
        {
            var s = db.Students.Find(id);

            if (s == null)
            {
                return RedirectToAction("Index");
            }


            ViewBag.ClassList = new SelectList(db.Classes, "Id", "Name");
            return View(s);
        }


        // POST: Student/AssignClass

        //[HttpPost]
        //public ActionResult AssignClass(Students student)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var classType = AssignType(student.Age);
        //        // If no suitable class is found for the student's age, return an error
        //        if (classType == null)
        //        {
        //            TempData["Info"] = "No suitable class found for this student's age.";
        //            return RedirectToAction("Index");
        //        }

        //        // Get the class based on the classType
        //        var classes = db.Classes.FirstOrDefault(c => c.ClassType == classType);

        //        // If class is not found, return an error
        //        if (classes == null)
        //        {
        //            TempData["Info"] = "No suitable class found for this student's age.";
        //            return RedirectToAction("Index");
        //        }

        //        // Check if the class has reached its capacity
        //        if (classes.Students.Count >= classes.Capacity)
        //        {
        //            TempData["Info"] = $"Class {classes.Name} is fully occupied.";
        //            return RedirectToAction("Index");
        //        }

        //        // Assign the student to the class
        //        //student.ClassId = classes.Id;
        //        student.Classes = classes;

        //        db.Students.Add(student);
        //        db.SaveChanges();

        //        TempData["Info"] = $"Student {student.Name} added to Class {classes.Name}.";

        //        return RedirectToAction("Index");
        //    }
        //    return View(student);
        //}

        [HttpPost]
        public IActionResult AssignClass(string id, string classesId)
        {
            var student = db.Students.FirstOrDefault(s => s.Id == id);

            if (student == null)
            {
                TempData["Info"] = $"Student {id} not found.";
                return RedirectToAction("Index");
            }

            // Update ClassesId for the student
            student.ClassesId = classesId;

            // Save changes
            db.SaveChanges();

            TempData["Info"] = "Class assigned successfully.";
            return RedirectToAction("Index");
        }



        // assign classType for classes based on student's age
        private string AssignType(int age)
        {
            if (age == 3 || age == 4)
            {
                return "K1";
            }
            else if (age == 5)
            {
                return "K2";
            }
            else if (age == 6)
            {
                return "K3";
            }
            else
            {
                return null;
            }
        }
    }
}
