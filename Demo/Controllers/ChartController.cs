using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.X509;
using System.Linq;
using static Demo.Helper;

namespace Demo.Controllers;

public class ChartController : Controller
{
    private readonly DB db;
    private readonly IWebHostEnvironment en;
    private readonly Helper hp;

    public ChartController(DB db, IWebHostEnvironment en, Helper hp)
    {
        this.db = db;
        this.en = en;
        this.hp = hp;
    }

    // GET: Home/Index
    public IActionResult Index()
    {
        return View();
    }

    // GET: Chart/Chart1
    public IActionResult Chart1()
    {
        return View();
    }

    // GET: Chart/Chart1Data
    public IActionResult Data1()
    {
        var data = db.Users
                      .AsEnumerable()
                      .GroupBy(u => u.Role)
                      .Select(g => new { Role = g.Key, Count = g.Count() })
                      .ToList();

        return Json(data);
    }

    // GET: Chart/Chart2
    public IActionResult Chart2()
    {
        return View();
    }

    // GET: Chart/Chart2Data
    public IActionResult Data2()
    {
        var users = db.Users.ToList();

        var dt = users
                 .GroupBy(u => u.GetType().Name)
                 .Select(g => new
                 {
                     Role = g.Key,
                     FemaleActive = g.Count(u => u.Gender == "F" && u.IsActive),
                     FemaleInactive = g.Count(u => u.Gender == "F" && !u.IsActive),
                     MaleActive = g.Count(u => u.Gender == "M" && u.IsActive),
                     MaleInactive = g.Count(u => u.Gender == "M" && !u.IsActive)
                 })
                 .OrderBy(x => x.Role)
                 .ToList();

        var result = dt.Select(x => new object[]
        {
        x.Role,
        x.FemaleActive, x.FemaleInactive,
        x.MaleActive, x.MaleInactive
        });

        return Json(result);
    }


    //GET: Chart/Chart3
    public IActionResult Chart3()
    {
        var users = db.Users.ToList();
        return View(users);
    }

    // GET: Chart/Chart3Data
    [HttpGet]
    public IActionResult Data3(string? role)
    {
        var data = db.Users
                     .AsEnumerable()
                     .Where(s => s.Role == role || role == null)
                     .GroupBy(s => s.Gender)
                     .OrderBy(g => g.Key)
                     .Select(g => new object[]
                     {
                         g.Key == "F" ? "Female" : "Male",
                         g.Count()
                     })
                     .ToList();

        return Json(data);
    }


    // GET: Chart/Demo6
    public IActionResult Chart4()
    {
        int minAge = 0;
        int maxAge = 0;

        if (db.Users.OfType<Parent>().Any())
        {
            minAge = db.Users.OfType<Parent>().Min(u => u.Age);
            maxAge = db.Users.OfType<Parent>().Max(u => u.Age);
        }

        ViewBag.AgeGroups = AgeGroupHelper.GetAgeGroups(minAge, maxAge);

        return View();
    }

    // GET: Chart/Data6
    [HttpGet]
    public IActionResult Data4()
    {
        var parents = db.Users.OfType<Parent>().ToList();

        var ageGroups = parents
            .GroupBy(p => (p.Age / 5) * 5)
            .OrderBy(g => g.Key)
            .Select(g => new
            {
                ageGroup = $"{g.Key} - {g.Key + 4}",
                count = g.Count()
            }).ToList();

        var averageCount = ageGroups.Average(g => g.count);

        return Json(new { ageGroups, averageCount });
    }


    // GET: Chart/Demo4
    public IActionResult Chart5()
    {
        return View();
    }

    // GET: Chart/Data5

    public IActionResult Data5(string? id)
    {
        DateTime date = id == null ? DateTime.Now : DateTime.Parse(id);
   
        var total = db.Students.GroupBy(s => s.ClassId);

        var dt = db.Students
            .Include(s => s.Class)
            .GroupBy(s => s.Class.Name)
            .Select(g => new
            {
                ClassName = g.Key,
                AttendanceCount = g.SelectMany(s => s.Attendances)
                                   .Count(a => a.DateTime.Date == date.Date && a.IsAttend),
                AbsenceCount = g.Count() - g.SelectMany(s => s.Attendances)
                                .Count(a => a.DateTime.Date == date.Date && a.IsAttend)
            })
            .ToList()
            .Select(g => new object[] { g.ClassName, g.AttendanceCount, g.AbsenceCount })
            .ToList();

        return Json(dt);
    }

    // GET: Chart/Chart6
    public IActionResult Chart6()
    {
        return View();
    }

    // GET: Chart/Data6
    public IActionResult Data6(string? id)
    {
        DateTime targetDate = id == null ? DateTime.Now : DateTime.Parse(id);
        var total = db.Students.GroupBy(s => s.ClassId);

        var dt = db.Students
        .Include(s => s.Class)
        .Include(s => s.Attendances)
        .Select(s => new
        {
            s.Id,
            s.Name,
            ClassName = s.Class.Name,
            AbsencesCount = hp.GetTotalDays(targetDate.Year, targetDate.Month) - s.Attendances
                .Count(a => a.DateTime.Year == targetDate.Year && a.DateTime.Month == targetDate.Month && a.IsAttend)
        })
        .OrderByDescending(s => s.AbsencesCount)
        .Take(10)
        .Select(s => new StudentAbsenceVM
        {
            StudentId = s.Id,
            StudentName = s.Name,
            ClassName = s.ClassName,
            AbsencesCount = s.AbsencesCount
        })
        .ToList()
        .Select(g => new object[] { g.StudentName, g.AbsencesCount })
        .ToList();

        return Json(dt);
    }

    public IActionResult Chart7()
    {
        return View();
    }

    // GET: Chart/Data7
    public IActionResult Data7()
    {
        var dt = db.Classes
                   .GroupBy(c => c.ClassType)
                   .OrderBy(g => g.Key)
                   .Select(g => new object[]
                   {
                           g.Key,  // ClassType
                           g.Count()  // Number of classes in each ClassType
                   });

        return Json(dt);
    }

    // GET: Chart/Data8
    public IActionResult Chart8()
    {
        return View();
    }

    // GET: Chart/Data8
    public IActionResult Data8()
    {
       
        var dt = db.Classes
                   .OrderBy(c => c.Id)
                   .Select(c => new object[]
                   {
                           c.Id,
                           c.Students.Count()
                   });

        return Json(dt);
    }

    

}
