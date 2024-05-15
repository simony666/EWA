using Microsoft.AspNetCore.Mvc;

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

        public IActionResult Index()
        {
            ViewBag.Classes = db.Classes;
            return View();
        }
    }
}
