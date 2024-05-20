using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.X509;
using System.Linq;

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

    // GET: Chart/Demo1
    public IActionResult Chart1()
    {
        return View();
    }

    // GET: Chart/Data1
    public IActionResult Data1()
    {
        var data = db.Users
                      .AsEnumerable()
                      .GroupBy(u => u.Role)
                      .Select(g => new { Role = g.Key, Count = g.Count() })
                      .ToList();

        return Json(data);
    }

    // GET: Chart/Demo4
    public IActionResult Chart2()
    {
        return View();
    }

    // GET: Chart/Data4
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

    // GET: Chart/Chart5
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

    // GET: Chart/Data5
    public IActionResult Data6(string? id, string? date)
    {
        DateTime targetDate = date == null ? DateTime.Now : DateTime.Parse(date);
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
        .Select(g => new object[] { g.StudentName, g.ClassName, g.AbsencesCount })
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
        // TODO: Return student count by program by gender --> JSON
        /*
        var dt = db.Students
                   .GroupBy(s => s.ProgramId)
                   .OrderBy(g => g.Key)
                   .Select(g => new object[]
                   {
                       g.Key,
                       g.Count(s => s.Gender == "F"),
                       g.Count(s => s.Gender == "M")
                   });
        */

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
