using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using TETHER.Models;
using TETHER.Models.Entities;
using TETHER.Data;
using TETHER.Models.ViewModels;

namespace TETHER.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        private IActionResult RequireLogin()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login");
            }
            return null;
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
            return View(new TeamMember { Role = null });
        }

        [HttpPost]
        public IActionResult Login(TeamMember model)
        {
            var member = _context.TeamMembers
                .Include(t => t.Role)
                .FirstOrDefault(x => x.PersonalGmail == model.PersonalGmail);

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

            HttpContext.Session.SetString("Role", member.Role?.Name ?? string.Empty);
            HttpContext.Session.SetInt32("UserId", member.Id);

            return RedirectToAction("Dashboard");
        }

        public IActionResult Dashboard()
        {
            var redirect = RequireLogin();
            if (redirect != null) return redirect;

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
            var redirect = RequireLogin();
            if (redirect != null) return redirect;

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
                Entries = new Dictionary<int, List<CalendarEntry>>()
            };

            int nextId = 1;

            void AddEntry(int day, string title, string priority, string status, string assignedTo)
            {
                if (day < 1 || day > model.DaysInMonth) return;

                if (!model.Entries.ContainsKey(day))
                    model.Entries[day] = new List<CalendarEntry>();

                model.Entries[day].Add(new CalendarEntry
                {
                    Id = nextId++,
                    Date = new DateTime(y, m, day),
                    Title = title,
                    Priority = priority,
                    Status = status,
                    AssignedTo = assignedTo
                });
            }

            AddEntry(1, "IAS1: Activity # 2_Final Term", "Medium", "Pending", "JOSIAH ZACHARY Q. SY");
            AddEntry(5, "CP1: Proposal Defense", "High", "Pending", "REINA CHLOE D. MAGPANTAY");
            AddEntry(9, "DM: Pass Previous Activies", "Medium", "Progress", "SARAH MAE D.C. HARINA");
            AddEntry(11, "IAS1: Role Play", "Medium", "Pending", "JOSIAH ZACHARY Q. SY");
            AddEntry(13, "DM: Activity #1", "Medium", "Pending", "SARAH MAE D.C. HARINA");
            AddEntry(14, "CW: Module 5 Quiz", "Low", "Pending", "JOHANNA ANGELA P. ESTALILLA");
            AddEntry(14, "CW: Online Discussion Board 5", "Low", "Pending", "JOHANNA ANGELA P. ESTALILLA");
            AddEntry(16, "ADET: Activity 6 - Project Documentation", "High", "Progress", "REINA CHLOE D. MAGPANTAY");
            AddEntry(16, "ADET: Final Project", "High", "Pending", "REINA CHLOE D. MAGPANTAY");
            AddEntry(17, "IAS1: Activity # 3_Final Term", "Medium", "Pending", "JOSIAH ZACHARY Q. SY");
            AddEntry(20, "CP1: Revision Presentation", "High", "Pending", "REINA CHLOE D. MAGPANTAY");
            AddEntry(22, "CW: Final Exam", "Low", "Pending", "JOHANNA ANGELA P. ESTALILLA");

            return View(model);
        }

        public IActionResult Team()
        {
            var redirect = RequireLogin();
            if (redirect != null) return redirect;

            var Team = _context.TeamMembers
                .Include(t => t.Role)
                .ToList();

            return View(Team);
        }

        public IActionResult Profile_Hanna()
        {
            var redirect = RequireLogin();
            if (redirect != null) return redirect;
            return View();
        }

        public IActionResult Profile_Rei()
        {
            var redirect = RequireLogin();
            if (redirect != null) return redirect;
            return View();
        }

        public IActionResult Profile_Sarah()
        {
            var redirect = RequireLogin();
            if (redirect != null) return redirect;
            return View();
        }

        public IActionResult Profile_Zach()
        {
            var redirect = RequireLogin();
            if (redirect != null) return redirect;
            return View();
        }

        [HttpGet]
        public IActionResult AddTask()
        {
            var redirect = RequireLogin();
            if (redirect != null) return redirect;

            var model = new TaskFormViewModel
            {
                AvailableMembers = _context.TeamMembers.ToList()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddTask(TaskFormViewModel model)
        {
            var redirect = RequireLogin();
            if (redirect != null) return redirect;

            var userId = HttpContext.Session.GetInt32("UserId");

            if (!ModelState.IsValid)
            {
                model.AvailableMembers = _context.TeamMembers.ToList();
                return View(model);
            }

            var priorityLevel = _context.PriorityLevels
                .FirstOrDefault(p => p.Name == model.SelectedPriority);

            var status = _context.Statuses
                .FirstOrDefault(s => s.Name == model.SelectedStatus);

            if (priorityLevel == null || status == null)
            {
                ModelState.AddModelError("", "Invalid priority or status selected.");
                model.AvailableMembers = _context.TeamMembers.ToList();
                return View(model);
            }

            var task = new TaskItem
            {
                Name = model.Name,
                PriorityLevelId = priorityLevel.Id,
                StartDate = model.StartDate,
                Deadline = model.Deadline,
                DocsLink = model.DocsLink ?? string.Empty,
                Description = model.Description ?? string.Empty,
                StatusId = status.Id,
                PmId = userId!.Value
            };

            foreach (var memberId in model.SelectedMembers)
            {
                task.Assignments.Add(new TaskItemAssignment
                {
                    AssignedTo = memberId,
                    AssignedAt = DateTime.Now
                });
            }

            _context.TaskItems.Add(task);
            _context.SaveChanges();

            return RedirectToAction("Dashboard");
        }

        [HttpGet]
        public IActionResult UpdateTask(int id)
        {
            var redirect = RequireLogin();
            if (redirect != null) return redirect;

            var task = _context.TaskItems
                .Include(t => t.Assignments)
                .Include(t => t.PriorityLevel)
                .Include(t => t.Status)
                .FirstOrDefault(t => t.Id == id);

            if (task == null) return NotFound();

            ViewBag.IsPM = HttpContext.Session.GetString("Role") == "Project Manager";

            var model = new TaskFormViewModel
            {
                Id = task.Id,
                Name = task.Name,
                SelectedPriority = task.PriorityLevel?.Name ?? string.Empty,
                StartDate = task.StartDate,
                Deadline = task.Deadline,
                DocsLink = task.DocsLink,
                Description = task.Description,
                SelectedStatus = task.Status?.Name ?? "Pending",
                SelectedMembers = task.Assignments.Select(a => a.AssignedTo).ToList(),
                AvailableMembers = _context.TeamMembers.ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateTask(TaskFormViewModel model)
        {
            var redirect = RequireLogin();
            if (redirect != null) return redirect;

            var isPM = HttpContext.Session.GetString("Role") == "Project Manager";

            if (!ModelState.IsValid)
            {
                model.AvailableMembers = _context.TeamMembers.ToList();
                ViewBag.IsPM = isPM;
                return View(model);
            }

            var task = _context.TaskItems
                .Include(t => t.Assignments)
                .FirstOrDefault(t => t.Id == model.Id);

            if (task == null) return NotFound();

            var status = _context.Statuses
                .FirstOrDefault(s => s.Name == model.SelectedStatus);

            if (status == null)
            {
                ModelState.AddModelError("", "Invalid status selected.");
                model.AvailableMembers = _context.TeamMembers.ToList();
                ViewBag.IsPM = isPM;
                return View(model);
            }

            // Everyone can update status
            task.StatusId = status.Id;

            // Only PM can update everything else
            if (isPM)
            {
                var priorityLevel = _context.PriorityLevels
                    .FirstOrDefault(p => p.Name == model.SelectedPriority);

                if (priorityLevel == null)
                {
                    ModelState.AddModelError("", "Invalid priority selected.");
                    model.AvailableMembers = _context.TeamMembers.ToList();
                    ViewBag.IsPM = isPM;
                    return View(model);
                }

                task.Name = model.Name;
                task.PriorityLevelId = priorityLevel.Id;
                task.StartDate = model.StartDate;
                task.Deadline = model.Deadline;
                task.DocsLink = model.DocsLink ?? string.Empty;
                task.Description = model.Description ?? string.Empty;

                task.Assignments.Clear();
                foreach (var memberId in model.SelectedMembers)
                {
                    task.Assignments.Add(new TaskItemAssignment
                    {
                        AssignedTo = memberId,
                        AssignedAt = DateTime.Now
                    });
                }
            }

            _context.SaveChanges();

            return RedirectToAction("Dashboard");
        }

        public IActionResult DoneTask()
        {
            var redirect = RequireLogin();
            if (redirect != null) return redirect;
            return View();
        }
    }
}