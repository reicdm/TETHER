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
            return View(new Auth
            {
                Role = role
            });
        }

        [HttpPost]
        public IActionResult Login(Auth login)
        {
            var authMember = _context.Auths
                .FirstOrDefault(x => x.Email == login.Email);

            if (authMember == null)
            {
                ViewBag.Error = "Email not found.";
                return View(login);
            }

            if (authMember.Password != login.Password)
            {
                ViewBag.Error = "Incorrect password.";
                return View(login);
            }

            return RedirectToAction("Dashboard", new { role = authMember.Role });
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
