using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
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

            return RedirectToAction("Dashboard", new { role = member.Role?.Name });
        }

        public IActionResult Dashboard(string role)
        {
            ViewBag.Role = string.IsNullOrEmpty(role) ? "Member" : role;

            int currentUserId = 1;
            ViewBag.CurrentUserId = currentUserId;

            var Tasks = _context.TaskItems
                .Include(t => t.Status)
                .Include(t => t.PriorityLevel)
                .Include(t => t.Assignments)
                    .ThenInclude(a => a.TeamMember)
                .ToList();

            return View(Tasks);
        }

        public IActionResult Team()
        {
            var Team = _context.TeamMembers
                .Include(t => t.Role)
                .ToList(); 

            return View(Team);
        }
    }
}
