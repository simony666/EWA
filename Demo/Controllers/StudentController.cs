using Demo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using X.PagedList;
using System.Net.Mail;
using iText.Layout;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using iText.Layout.Element;
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

        public IActionResult StudentList(string? name, string? sort, string? dir, int page = 1)
        {
            // (1) Searching ------------------------
            ViewBag.Name = name = name?.Trim() ?? "";

            var searched = db.Students.Where(s => s.Name.Contains(name));

            // (2) Sorting --------------------------
            ViewBag.Sort = sort;
            ViewBag.Dir = dir;

            Func<Student, object> fn = sort switch
            {
                "Id" => s => s.Id,
                "Name" => s => s.Name,
                "Gender" => s => s.Gender,
                "Age" => s => s.Age, 
                "Class Id" => s => s.ClassId,
                _ => s => s.Id,
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

            if (Request.IsAjax())
            {
                return PartialView("_StudentList", model);
            }

            ViewBag.ClassList = db.Classes.ToDictionary(c => c.Id, c => c.Name);
            return View(model);           
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Student/Create
        [HttpPost]
        public ActionResult Create(CreateStudentsVM vm)
        {
            if (ModelState.IsValid)
            {
                db.Tutors.Add(new()
                {
                    Id = vm.Id,
                    Email = vm.Email,
                    Hash = vm.Hash,
                    Name = vm.Name,
                    Gender = vm.Gender,
                    PhotoURL = vm.PhotoURL,
                    Age = vm.Age,
                    Phone = vm.Phone,

                });
                db.SaveChanges();
                TempData["Info"] = $"Student inserted.";
                //return RedirectToAction("Index");
            }

            ViewBag.Tutors = db.Tutors;
            return View(vm);
        }

        // GET: Student/AssignClass
        public IActionResult AssignClass(string? id)
        {
            var s = db.Students.Find(id);

            if (s == null)
            {
                return RedirectToAction("StudentList");
            }


            //dropdown list for class list
            ViewBag.ClassList = new SelectList(db.Classes.OrderBy(c => c.Id), "Id", "Name");
            return View(s);
        }


        [HttpPost]
        public IActionResult AssignClass(string id, string classId)
        {
            // Find the student by ID
            var student = db.Students.Find(id);

            if (student == null)
            {
                TempData["Info"] = $"Student {id} not found.";
                return RedirectToAction("StudentList");
            }

            // Find the class by classId
            var selectedClass = db.Classes.Find(classId);

            if (selectedClass == null)
            {
                TempData["Info"] = $"Class {classId} not found.";
                return RedirectToAction("StudentList");
            }

            // Validate age for the class
            bool isValidAge = false;
            switch (selectedClass.ClassType)
            {
                case "K1":
                    isValidAge = student.Age >= 3 && student.Age <= 4;
                    break;
                case "K2":
                    isValidAge = student.Age == 5;
                    break;
                case "K3":
                    isValidAge = student.Age == 6;
                    break;
                default:
                    TempData["Info"] = "Invalid class type.";
                    return RedirectToAction("StudentList");
            }

            // If age is not valid for the selected class, return with error message
            if (!isValidAge)
            {
                TempData["Info"] = $"Student age ({student.Age} years old) is not within the allowed range for class {selectedClass.Name}.";
                return RedirectToAction("StudentList");
            }

            // Check if class capacity will be exceeded (only allows 20 students in a class)
            if (selectedClass.Capacity > 21)
            {
                TempData["Info"] = $"{selectedClass.Name} is fully occupied. Please select a new class or create a new class to assign students.";
                return RedirectToAction("StudentList");
            }

            // Increase the class capacity by 1
            selectedClass.Capacity += 1;

            // Update ClassId for the student
            student.ClassId = classId;

            // Save changes
            db.SaveChanges();

            TempData["Info"] = "Class assigned successfully.";
            return RedirectToAction("Index");
        }


            // GET: Student/ViewTimetable
            public IActionResult ViewTimetable(string id)
        {
            //id is student id
            var parent = db.Parents.FirstOrDefault(p => p.Students.Any(s => s.Id == id));


            var student = db.Students
                            .Include(s => s.Class)
                                .ThenInclude(cs => cs.ClassSubjects)
                                    .ThenInclude(c => c.Subject)
                                        .ThenInclude(t => t.Tutor)
                            .FirstOrDefault(s => s.Id == id);

            if (student == null)
            {
                return RedirectToAction("StudentList");
            }
            
            SendWarningEmail(parent, student);
            return View(student);
        }

        private void SendWarningEmail(Parent m, Student student)
        {
            var mail = new MailMessage();
            mail.To.Add(new MailAddress(m.Email, m.Name));
            mail.Subject = "Your child timetable";
            mail.IsBodyHtml = true;

            var now = DateTime.Now;


            var c = student.Class;
            string[] dayOfWeek = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" };

            // Construct the email body
            var emailBody = $@"
            <html>
                <body>
                    <p>Dear {m.Name},</p>
                    <p>Here is the timetable:</p>
<table border='1' class='table' style='margin-top: 10px'>
    <thead>
        <tr><th colspan='21'>{c.Name} - {student.Name}'s Timetable</th></tr>
        <tr>
            <th>Time</th>";

            for (int column = 8; column <= 20; column++)
            {
                emailBody += $"<th>{column}:00</th>";
            }

            emailBody += $@"
        </tr>
    </thead>
    <tbody>";


            for (int day = 0; day < 5; day++)
            {
                emailBody += $@"
    <tr>
        <th>{dayOfWeek[day]}</th>";

                int column = 8;
                while (column <= 20)
                {
                    bool courseFound = false;
                    string courseId = "";
                    string courseName = "";
                    string tutorName = "";
                    int duration = 0;

                    foreach (var cs in c.ClassSubjects)
                    {
                        if (dayOfWeek[day].Equals(cs.DayOfWeek) && cs.StartTime.Hours == column)
                        {
                            courseId = cs.Subject.Id;
                            courseName = cs.Subject.Name;
                            duration = cs.Duration;
                            tutorName = cs.Subject.Tutor.Name;

                            courseFound = true;
                            column += duration + 1;
                            break;
                        }
                    }

                    if (courseFound)
                    {
                        emailBody += $@"
            <td style='text-align: center; background-color:#f0f0f0; font-size:12px;' colspan='{duration + 1}'>
                <span style='font-weight:bold;'>{courseId}</span> <br>
                <span style='font-weight:bold;'>Subject Name:</span>{courseName}<br>
                <span style='font-weight:bold;'>Tutor Name:</span> {tutorName}
                <br>
            </td>";
                    }
                    else
                    {
                        emailBody += "<td></td>";
                        column++;
                    }
                }

                emailBody += "</tr>";
            }

            emailBody += @"
    </tbody>
</table>

            
        </body>
        </html>";
            mail.Body = emailBody;
            hp.SendEmail(mail);
        }
    }
}

