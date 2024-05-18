using Demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Net.Mail;
using static QRCoder.PayloadGenerator;

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
                Id = "T001",
                Name = "Tutor 1",
                Email = "T1@gmail.com",
                Hash = hp.HashPassword("123456"),
                Gender = "M",
                Age = 40,
                Phone = "0912345678",
            });
            db.Tutors.Add(new Tutor()
            {
                Id = "T002",
                Name = "Tutor 2",
                Email = "T2@gmail.com",
                Hash = hp.HashPassword("123456"),
                Gender = "M",
                Age = 39,
                Phone = "0912345678",
            });
            db.Tutors.Add(new Tutor()
            {
                Id = "T003",
                Name = "Tutor 3",
                Email = "T3@gmail.com",
                Hash = hp.HashPassword("123456"),
                Gender = "F",
                Age = 38,
                Phone = "0912345678",
            });
            db.Classes.Add(new Class
            {
                Id = "C001",
                Name = "1 Alpha",
                Capacity = 40,
                ClassType = "T",
            });
            db.Classes.Add(new Class
            {
                Id = "C002",
                Name = "1 Best",
                Capacity = 40,
                ClassType = "T",
            });
            db.Classes.Add(new Class
            {
                Id = "C003",
                Name = "1 Credit",
                Capacity = 40,
                ClassType = "T",
            });
            db.Classes.Add(new Class
            {
                Id = "C004",
                Name = "2 Alpha",
                Capacity = 40,
                ClassType = "T",
            });
            db.Classes.Add(new Class
            {
                Id = "C005",
                Name = "2 Best",
                Capacity = 40,
                ClassType = "T",
            });
            db.Classes.Add(new Class
            {
                Id = "C006",
                Name = "2 Credit",
                Capacity = 40,
                ClassType = "T",
            });
            db.Subjects.Add(new Subject()
            {
                Id = "SU001",
                Name = "Bahasa Melayu",
                Fees = 300,
                TutorId = "T001"
            });
            db.Subjects.Add(new Subject()
            {
                Id = "SU002",
                Name = "Math",
                Fees = 320,
                TutorId = "T002"
            });
            db.Subjects.Add(new Subject()
            {
                Id = "SU003",
                Name = "English",
                Fees = 320,
                TutorId = "T003"
            });
            db.ClassesSubjects.Add(new ClassSubject() {
                StartTime = new TimeSpan(8, 0, 0),
                EndTime = new TimeSpan(10, 0, 0),
                Duration = 2,
                DayOfWeek = "Monday",
                SubjectId = "SU001",
                ClassId = "C001",
            });
            db.ClassesSubjects.Add(new ClassSubject()
            {
                StartTime = new TimeSpan(8, 0, 0),
                EndTime = new TimeSpan(10, 0, 0),
                Duration = 2,
                DayOfWeek = "Tuesday",
                SubjectId = "SU002",
                ClassId = "C002",
            });
            db.ClassesSubjects.Add(new ClassSubject()
            {
                StartTime = new TimeSpan(8, 0, 0),
                EndTime = new TimeSpan(10, 0, 0),
                Duration = 2,
                DayOfWeek = "Wednesday",
                SubjectId = "SU003",
                ClassId = "C003",
            });
            db.ClassesSubjects.Add(new ClassSubject()
            {
                StartTime = new TimeSpan(10, 0, 0),
                EndTime = new TimeSpan(12, 0, 0),
                Duration = 2,
                DayOfWeek = "Thursday",
                SubjectId = "SU001",
                ClassId = "C004",
            });
            db.ClassesSubjects.Add(new ClassSubject()
            {
                StartTime = new TimeSpan(10, 0, 0),
                EndTime = new TimeSpan(12, 0, 0),
                Duration = 2,
                DayOfWeek = "Friday",
                SubjectId = "SU002",
                ClassId = "C005",
            });
            db.ClassesSubjects.Add(new ClassSubject()
            {
                StartTime = new TimeSpan(10, 0, 0),
                EndTime = new TimeSpan(12, 0, 0),
                Duration = 2,
                DayOfWeek = "Monday",
                SubjectId = "SU003",
                ClassId = "C006",
            });

            db.ClassesSubjects.Add(new ClassSubject()
            {
                StartTime = new TimeSpan(14, 0, 0),
                EndTime = new TimeSpan(16, 0, 0),
                Duration = 2,
                DayOfWeek = "Monday",
                SubjectId = "SU001",
                ClassId = "C002",
            });
            db.ClassesSubjects.Add(new ClassSubject()
            {
                StartTime = new TimeSpan(14, 0, 0),
                EndTime = new TimeSpan(16, 0, 0),
                Duration = 2,
                DayOfWeek = "Tuesday",
                SubjectId = "SU002",
                ClassId = "C003",
            });
            db.ClassesSubjects.Add(new ClassSubject()
            {
                StartTime = new TimeSpan(14, 0, 0),
                EndTime = new TimeSpan(16, 0, 0),
                Duration = 2,
                DayOfWeek = "Wednesday",
                SubjectId = "SU003",
                ClassId = "C004",
            });
            db.ClassesSubjects.Add(new ClassSubject()
            {
                StartTime = new TimeSpan(14, 0, 0),
                EndTime = new TimeSpan(16, 0, 0),
                Duration = 2,
                DayOfWeek = "Thursday",
                SubjectId = "SU001",
                ClassId = "C005",
            });
            db.ClassesSubjects.Add(new ClassSubject()
            {
                StartTime = new TimeSpan(14, 0, 0),
                EndTime = new TimeSpan(16, 0, 0),
                Duration = 2,
                DayOfWeek = "Friday",
                SubjectId = "SU002",
                ClassId = "C006",
            });
            db.ClassesSubjects.Add(new ClassSubject()
            {
                StartTime = new TimeSpan(14, 0, 0),
                EndTime = new TimeSpan(16, 0, 0),
                Duration = 2,
                DayOfWeek = "Monday",
                SubjectId = "SU003",
                ClassId = "C001",
            });
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult DB1()
        {
            db.Students.Add(new Student()
            {
                Id = "S001",
                Name = "Student 1",
                Gender = "M",
                Age = 4,
                ClassId = "C001",
            });
            db.Students.Add(new Student()
            {
                Id = "S002",
                Name = "Student 2",
                Gender = "M",
                Age = 4,
                ClassId = "C001",
            });
            db.Students.Add(new Student()
            {
                Id = "S003",
                Name = "Student 3",
                Gender = "M",
                Age = 4,
                ClassId = "C001",
            });
            db.Students.Add(new Student()
            {
                Id = "S004",
                Name = "Student 4",
                Gender = "F",
                Age = 5,
                ClassId = "C002",
            });
            db.Students.Add(new Student()
            {
                Id = "S005",
                Name = "Student 5",
                Gender = "M",
                Age = 5,
                ClassId = "C002",
            });
            db.Students.Add(new Student()
            {
                Id = "S006",
                Name = "Student 6",
                Gender = "M",
                Age = 5,
                ClassId = "C002",
            });
            db.Students.Add(new Student()
            {
                Id = "S007",
                Name = "Student 7",
                Gender = "F",
                Age = 6,
                ClassId = "C003",
            });
            db.Students.Add(new Student()
            {
                Id = "S008",
                Name = "Student 8",
                Gender = "M",
                Age = 6,
                ClassId = "C003",
            });
            db.Students.Add(new Student()
            {
                Id = "S009",
                Name = "Student 9",
                Gender = "M",
                Age = 6,
                ClassId = "C003",
            });
            db.Students.Add(new Student()
            {
                Id = "S010",
                Name = "Student 10",
                Gender = "F",
                Age = 5,
                ClassId = "C004",
            });
            db.Students.Add(new Student()
            {
                Id = "S011",
                Name = "Student 11",
                Gender = "M",
                Age = 5,
                ClassId = "C004",
            });
            db.Students.Add(new Student()
            {
                Id = "S012",
                Name = "Student 12",
                Gender = "M",
                Age = 5,
                ClassId = "C005",
            });
            db.Students.Add(new Student()
            {
                Id = "S013",
                Name = "Student 13",
                Gender = "F",
                Age = 5,
                ClassId = "C005",
            });
            db.Students.Add(new Student()
            {
                Id = "S014",
                Name = "Student 14",
                Gender = "M",
                Age = 5,
                ClassId = "C005",
            });
            db.Students.Add(new Student()
            {
                Id = "S015",
                Name = "Student 15",
                Gender = "M",
                Age = 5,
                ClassId = "C006",
            });
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult resetDB()
        {
            db.ClassesSubjects.ExecuteDelete();
            db.Subjects.ExecuteDelete();
            db.Students.ExecuteDelete();
            db.Tutors.ExecuteDelete();
            db.Classes.ExecuteDelete();
            db.SaveChanges();


            return RedirectToAction("Index");
        }

        public ActionResult Index(string? id)
        {
            if (id != null)
            {
                DateTime dt = DateTime.Parse(id);
                ViewBag.date = dt.ToString("yyyy-MM-dd");
                ViewBag.SubjectClass = db.ClassesSubjects.Include(cs => cs.Class)
                                        .Include(cs => cs.Subject)
                                        .Where(cs => cs.Subject.Tutor.Email == User.Identity!.Name && cs.DayOfWeek.Equals(dt.DayOfWeek.ToString()));
            }
            else
            {
                var dayOfWeekOrder = new Dictionary<string, int>
                                    {
                                        { "Monday", 0 },
                                        { "Tuesday", 1 },
                                        { "Wednesday", 2 },
                                        { "Thursday", 3 },
                                        { "Friday", 4 },
                                        { "Saturday", 5 },
                                        { "Sunday", 6 }
                                    };

                ViewBag.SubjectClass = db.ClassesSubjects.Where(cs => cs.Subject.Tutor.Email == User.Identity!.Name).
                                Include(cs => cs.Class).Include(cs => cs.Subject)
                                .AsEnumerable()
                                .OrderBy(cs => dayOfWeekOrder[cs.DayOfWeek]);
            }


            return View();
        }

        public ActionResult Attendance(string? id, string? date)
        {
            var model = new List<StudentAttendanceVM>();

            var students = id == null ? db.Students.ToList() : db.Students.Where(s => s.Id == id).ToList();
            var totalClasses = GetTotalDays(DateTime.Now.Year, DateTime.Now.Month);
            //totalClasses = 3;

            foreach (var stu in students)
            {
                var attendances = db.Attendances.Where(a => a.Student.Id == stu.Id);

                if (date != null)
                {
                    DateTime dt = DateTime.Parse(date);
                    attendances = attendances.Where(a => a.DateTime.Year == dt.Year && a.DateTime.Month == dt.Month);
                }
                else
                {
                    DateTime dt = DateTime.Now;
                    attendances = attendances.Where(a => a.DateTime.Year == dt.Year && a.DateTime.Month == dt.Month);
                }

                var attend = attendances.Count();
                var percentage = (double)attend / totalClasses * 100;


                model.Add(new StudentAttendanceVM()
                {
                    Id = stu.Id,
                    Name = stu.Name,
                    Percentage = percentage,
                });
                    
            }   
                return View(model);
        }

        public int GetTotalDays(int year, int month)
            {
            DateTime today = DateTime.Now;
            int totalDays = year == today.Year && month == today.Month ? today.Day : DateTime.DaysInMonth(year, month);
            int workingDays = 0;

            for (int day = 1; day <= totalDays; day++)
                {
                DateTime currentDate = new DateTime(year, month, day);
                if (currentDate.DayOfWeek != DayOfWeek.Saturday && currentDate.DayOfWeek != DayOfWeek.Sunday)
                {
                    workingDays++;
                }
            }

            return workingDays;
        }

        [HttpPost]
        public IActionResult SendWarning(string id, double percentage) {
            //id is student id
            var parent = db.Parents.FirstOrDefault(p => p.StudentList.Contains(id));

            var stu = db.Students.FirstOrDefault(s => s.Id == id);
            List<StudentAttendanceVM> stuVM = new List<StudentAttendanceVM>();
            stuVM.Add(new StudentAttendanceVM()
            {
                Id = stu.Id,
                Name = stu.Name,
                Percentage = percentage,
            });
            SendWarningEmail(parent, stuVM);


            return Content("Successfully Send The Warning Letter");
        }

        private void SendWarningEmail(Parent m, StudentAttendanceVM[] Student)
        {
            var mail = new MailMessage();
            mail.To.Add(new MailAddress(m.Email, m.Name));
            mail.Subject = "-.-.  Warning! Attendance Is Low!  .-.-";
            mail.IsBodyHtml = true;

            var now = DateTime.Now;


            var path = Path.Combine(en.WebRootPath, "photos", m.PhotoURL);

            var att = new Attachment(path);
            mail.Attachments.Add(att);
            att.ContentId = "photo";


            // Construct the email body
            var emailBody = $@"
            <html>
                <body>
                    <img src='cid:photo' style='width: 200px; height: 200px; border: 1px solid #333'>
                    <p>Dear {m.Name},</p>
                    <p>Your children at Eden Academy have low attendance:</p>
                    <table>
                        <tr>
                            <td>Student ID</td>
                            <td>Student Name</td>
                            <td>Percentage</td>
                        </tr>";

            foreach (var u in Student)
            {
                emailBody += $@"
                <tr>
                    <td>{u.Id}</td>
                    <td>{u.Name}</td>
                    <td>{u.Percentage}</td>
                </tr>";
            }

            emailBody += $@"
            </table>
            <p>Please Take Particular Action On This Issue! Thank!</p>
            <p>From, 🐱 Eden Academy Admin</p>
            <br/>
            <p>Email Generate On {now.ToString("f")}.</p>
            
        </body>
        </html>";
            mail.Body = emailBody;

            hp.SendEmail(mail);
        }



        public ActionResult Detail(int id, string date)
        {
            DateTime dt = DateTime.Parse(date);
            var model = db.ClassesSubjects.Where(cs => cs.Id == id).Include(cs => cs.Class).Include(cs => cs.Subject).FirstOrDefault();
            ViewBag.StudentList = db.Students.Where(s => s.Class == model.Class);
            ViewBag.Attendance = db.Attendances.Where(a => a.DateTime.Date == dt.Date && a.Class == model.Class).Include(a=>a.Student).ToList();
            ViewBag.Date = date;
            return View(model);
        }

        [HttpPost]
        public IActionResult Mark(string id, int Classid, string date, bool ischecked)
        {
            DateTime dt = DateTime.Parse(date); 
            var ClassSubjects = db.ClassesSubjects.Where(cs => cs.Id == Classid).Include(cs => cs.Class).Include(cs => cs.Subject).FirstOrDefault();
            var ClassId = ClassSubjects!.Class.Id;
            var att = db.Attendances.FirstOrDefault(a => a.Student.Id == id && a.Class.Id == ClassId && a.DateTime.Date == dt.Date);
            var stu = db.Students.FirstOrDefault(s => s.Id == id);

            if (att == null) {
                db.Attendances.Add(new Attendance()
                {
                    IsAttend = true,
                    MarkTime = DateTime.Now,
                    DateTime = dt.Date,
                    ClassId = ClassId,
                    Student = stu,
                });
            }
            else
            {
                if (att.IsAttend !=  ischecked) {
                    att.IsAttend = ischecked;
                    att.MarkTime = DateTime.Now;
                }
            }
            db.SaveChanges();

            return RedirectToAction("Detail", new { id = Classid, date = date });
        }

        [HttpPost]
        public IActionResult MarkMany(string[] ids, int Classid, string date)
        {
            DateTime dt = DateTime.Parse(date);
            var ClassSubjects = db.ClassesSubjects.Where(cs => cs.Id == Classid).Include(cs => cs.Class).Include(cs => cs.Subject).FirstOrDefault();
            var ClassId = ClassSubjects!.Class.Id;
            var studentList = db.Students.Where(s => s.Class.Id == ClassId);

            var attList = db.Attendances.Where(a => ids.Contains(a.Student.Id) && a.Class.Id == ClassId && a.DateTime.Date == dt.Date).ToList();
            foreach (Student stu in studentList) {
                var att = attList.FirstOrDefault(a => a.Student.Id == stu.Id);
                if (att == null) {
                    db.Attendances.Add(new Attendance()
                    {
                        IsAttend = true,
                        MarkTime = DateTime.Now,
                        DateTime = dt.Date,
                        ClassId = ClassId,
                        Student = stu,
                    });
                }
                else
                {
                    att.IsAttend = true;
                    db.Attendances.Update(att);
                    
                }
            }
            db.SaveChanges();

            return RedirectToAction("Detail", new { id = Classid,date = date});
        }


        public IActionResult QR(string? id)
        {
            var model = db.Students.ToList() ;
            if (id != null)
            { 
                model = db.Students.Where(s => s.ClassId == id).ToList();
            }

            return View(model);
        }

        public IActionResult Scan()
        {
            
            return View();
        }

        [HttpPost]
        public IActionResult Scan(string id)
        {

            DateTime dt = DateTime.Parse("20 May 2024");
            var stu = db.Students.Where(s => s.Id == id).Include(s => s.Class).FirstOrDefault();
            if (stu == null) { return Content("Invalid ID"); }

            var Classid = stu.Class.Id;
            var ClassSubjects = db.ClassesSubjects.Where(cs => cs.Class.Id.ToString() == Classid).Include(cs => cs.Class).Include(cs => cs.Subject).FirstOrDefault();
            if (ClassSubjects == null) { return Content("Class Not Found"); }
            var ClassId = ClassSubjects!.Class.Id;
            var att = db.Attendances.FirstOrDefault(a => a.Student.Id == id && a.Class.Id == ClassId && a.DateTime.Date == dt.Date);
            
            
            if (att == null)
            {
                db.Attendances.Add(new Attendance()
                {
                    IsAttend = true,
                    MarkTime = DateTime.Now,
                    DateTime = dt.Date,
                    ClassId = ClassId,
                    Student = stu,
                });
            }
            else
            {
                if (att.IsAttend != true)
                {
                    att.IsAttend = true;
                    att.MarkTime = DateTime.Now;
                }
            }
            db.SaveChanges();
            return Content($"Success! [{stu.Name}] {stu.Age} Year's Old  => 【{ClassSubjects.Subject.Name}】");
        }

    }
}
