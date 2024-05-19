using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers;

public class HomeController : Controller
{
    private readonly DB db;
    private readonly IWebHostEnvironment en;
    private readonly Helper hp;

    public HomeController(DB db, IWebHostEnvironment en, Helper hp)
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

    // GET: Home/Map
    public IActionResult Map()
    {
        var url = Url.Action("Index", "Home", null, "https");
        var model = new MapVM { Url = url };
        return View(model);
    }

    public IActionResult Chart3()
    {
        var users = db.Users.ToList();
        return View(users);
    }

    // GET: Chart/Chart3Data
    [HttpGet]
    public IActionResult Chart3Data(string? role)
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

}
