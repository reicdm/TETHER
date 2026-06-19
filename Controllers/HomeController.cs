using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Globalization;
using TETHER.Models;
using TETHER.Models.Entities;
using TETHER.Data;

namespace TETHER.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index() { return View(); }
        public IActionResult Privacy() { return View(); }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult Login(string role)
        {
            ViewBag.Role = string.IsNullOrEmpty(role) ? "Member" : role;

            return View(new TeamMember{ Role = null });
        }

        [HttpPost]
        public IActionResult Login(TeamMember model)
        {

            var member = _context.TeamMembers
                .Include(t => t.Role)
                .FirstOrDefault(x =>
                    x.PersonalGmail == model.PersonalGmail);

            if (member == null)
            {
                ViewBag.Error = "Email not found.";
                return View(model);
            }

            if (member.Password != model.Password)
            {
                ViewBag.Error = "Incorrect password.";
                return View(model);
            }

            HttpContext.Session.SetString("Role", member.Role?.Name);
            HttpContext.Session.SetInt32("UserId", member.Id);

            return RedirectToAction("Dashboard");
        }

        public IActionResult Dashboard()
        {
            var role = HttpContext.Session.GetString("Role");
            var userId = HttpContext.Session.GetInt32("UserId");

            ViewBag.Role = role;
            ViewBag.CurrentUserId = userId;

            var Tasks = _context.TaskItems
                .Include(t => t.Status)
                .Include(t => t.PriorityLevel)
                .Include(t => t.Assignments)
                    .ThenInclude(a => a.TeamMember)
                .ToList();

            return View(Tasks);
        }
        public IActionResult Calendar(int? year, int? month) 
        {
            int y = year ?? DateTime.Today.Year;
            int m = month ?? DateTime.Today.Month;

            var model = new CalendarViewModel
            {
                Year = y,
                Month = m,
                Prev = new DateTime(y, m, 1).AddMonths(-1),
                Next = new DateTime(y, m, 1).AddMonths(1),
                DaysInMonth = DateTime.DaysInMonth(y, m),
                FirstDayOfWeek = (int)new DateTime(y, m, 1).DayOfWeek,
                Entries = new Dictionary<int, CalendarEntry>()
            };

            return View(model);
        }
        public IActionResult Team()
        {
            var Team = _context.TeamMembers
                .Include(t => t.Role)
                .ToList(); 

            return View(Team);
        }
        public IActionResult Profile_Hanna() { return View(); }
        public IActionResult Profile_Rei() { return View(); }
        public IActionResult Profile_Sarah() { return View(); }
        public IActionResult Profile_Zach() { return View(); }

        public IActionResult AddTask() { return View(); }

        public IActionResult UpdateTask() { return View(); }
    }
}
