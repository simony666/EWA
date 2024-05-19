using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
}
