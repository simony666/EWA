using Demo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace Demo.Controllers
{
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

        public IActionResult Chart1()
        {
            return View();
        }

        // GET: Chart/Data1
        public IActionResult Data1()
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

        public IActionResult Chart2()
        {
            return View();
        }

        // GET: Chart/Data2
        public IActionResult Data2()
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
    }
}
