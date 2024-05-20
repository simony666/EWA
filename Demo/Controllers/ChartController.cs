using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

    // GET: Chart/Data4
    public IActionResult Data5()
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
}
