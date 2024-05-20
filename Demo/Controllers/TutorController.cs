using Demo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using System.Net.Mail;

namespace Demo.Controllers
{
    public class TutorController : Controller
    {
        private readonly DB db;
        private readonly IWebHostEnvironment en;
        private readonly Helper hp;

        public TutorController(DB db, IWebHostEnvironment en, Helper hp)
        {
            this.db = db;
            this.en = en;
            this.hp = hp;
        }

        public ActionResult Create()
        {
            return View();
        }


            // POST: Tutor/Create
            [HttpPost]
        public ActionResult Create(CreateTutorsVM vm)
        {
            if (ModelState.IsValid)
            {
                db.Tutors.Add(new()
                {
                    Id = vm.Id,
                    Email = vm.Email,
                    Hash = vm.Hash,
                    Name = vm.Name,
                    Gender =vm.Gender,
                    PhotoURL = vm.PhotoURL,
                    Age = vm.Age,
                    Phone = vm.Phone,
                   
                });
                db.SaveChanges();
                TempData["Info"] = $"Tutors inserted.";
                //return RedirectToAction("Index");
            }

            ViewBag.Tutors = db.Tutors;
            return View(vm);
        }

        // GET: Tutor/Index
        public IActionResult Index()
        {
            ViewBag.Tutors = db.Tutors;
            return View();
        }

        // GET: Tutor/ViewTimetable
        //public IActionResult ViewTimetable(string id)
        //{
        //    var tutorClasses = db.Tutors
        //              .Include(t => t.Subjects)
        //                  .ThenInclude(cs => cs.ClassesSubjects)
        //                      .ThenInclude(c => c.Class)
        //              .FirstOrDefault(t => t.Id == id);

        //    if (tutorClasses == null)
        //    {
        //        return RedirectToAction("Index");
        //    }
        //    return View(tutorClasses);
        //}

        // GET: Tutor/ViewTimetable
        public IActionResult ViewTimetable(string id)
        {

            var tutorClasses = db.Tutors
                      .Include(t => t.Subjects)
                          .ThenInclude(cs => cs.ClassesSubjects)
                              .ThenInclude(c => c.Class)
                      .FirstOrDefault(t => t.Id == id);

            if (tutorClasses == null)
            {
                return RedirectToAction("Index");
            }

            SendWarningEmail(tutorClasses);
            return View(tutorClasses);
        }

        private void SendWarningEmail(Tutor tutor)
        {
            var mail = new MailMessage();
            mail.To.Add(new MailAddress(tutor.Email, tutor.Name));
            mail.Subject = "Your timetable";
            mail.IsBodyHtml = true;

            var now = DateTime.Now;

            string[] dayOfWeek = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" };

            // Construct the email body
            var emailBody = $@"
            <html>
                <body>
                    <p>Dear {tutor.Name},</p>
                    <p>Here is the timetable:</p>
<table border='1' class='table' style='margin-top: 10px'>
    <thead>
        <tr><th colspan='21'>{tutor.Name}'s Timetable</th></tr>
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
                //Initialize the starting column for each day
                int column = 8;
                // Loop through each hour from 8 AM to 8 PM
                while (column <= 20)
                {

                    bool courseFound = false;
                    string subjectId = "";
                    string subjectName = "";
                    int duration = 0;
                    var className = "";
                    // Iterate through each subject to find matching courses for the current day and time
                    foreach (var s in tutor.Subjects)
                    {
                        foreach (var cs in s.ClassesSubjects)
                        {
                            if (dayOfWeek[day].Equals(cs.DayOfWeek) && cs.StartTime == TimeSpan.FromHours(column))
                            {

                                subjectId = s.Id;
                                subjectName = s.Name;
                                duration = cs.Duration;
                                className = cs.Class.Name;

                                courseFound = true;
                                column += duration + 1; // Increment by duration to move to the next available time slot
                                break;
                            }
                            //break;
                        }
                    }
                    if (courseFound)
                    {
                        emailBody += $@"
            <td style='text-align: center; background-color:#f0f0f0; font-size:12px;' colspan='{duration + 1}'>
                <span style='font-weight:bold;'>{subjectId}</span> <br>
                <span style='font-weight:bold;'>Subject Name:</span>{subjectName}<br>
                <span style='font-weight:bold;'>Class Name:</span> {className}
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
