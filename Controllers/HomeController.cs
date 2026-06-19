using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TETHER.Models;

namespace TETHER.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() { return View(); }
        public IActionResult Privacy() { return View(); }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Login(string role) { return View(); }

        public IActionResult Dashboard(string role)
        {
            ViewBag.Role = string.IsNullOrEmpty(role) ? "Member" : role;

            string currentUser = "REINA CHLOE D. MAGPANTAY";
            ViewBag.CurrentUser = currentUser;

            var dummyTask = new List<TaskItem>();

            // COLUMN 1: ASSIGNED TASKS (Global View - any incomplete tasks from any user)
            dummyTask.Add(new TaskItem { Id = 1, Title = "Setup PostGIS Maps Database", Status = "Pending", Priority = "High", AssignedTo = "JOHANNA ANGELA P.ESTALILLA" });
            dummyTask.Add(new TaskItem { Id = 2, Title = "Gather 2024 BFP Fire Statistics", Status = "Progress", Priority = "Medium", AssignedTo = "SARAH MAE D.C. HARINA" });
            dummyTask.Add(new TaskItem { Id = 3, Title = "Draft System Equip Requirements", Status = "Pending", Priority = "Low", AssignedTo = "JOSIAH ZACHARY Q. SY" });

            // COLUMN 2: PENDING (Assigned to current user, not started)
            for (int i = 4; i <= 6; i++)
                dummyTask.Add(new TaskItem { Id = i, Title = $"Sample Title {i}", Status = "Pending", Priority = "Medium", AssignedTo = currentUser });

            // COLUMN 3: IN PROGRESS (Active tasks of the current user)
            for (int i = 1; i <= 2; i++)
                dummyTask.Add(new TaskItem { Id = i + 9, Title = $"Sample Title {i}", Status = "Progress", Priority = "High", AssignedTo = currentUser });

            // COLUMN 4: DONE (Completed tasks of the current user)
            for (int i = 1; i <= 3; i++)
                dummyTask.Add(new TaskItem { Id = i + 11, Title = $"Sample Title {i}", Status = "Done", Priority = "Low", AssignedTo = currentUser });

            return View(dummyTask);
        }

        public IActionResult Team()
        {
            var Team = new List<TeamMember>
            {
                new TeamMember 
                { 
                    Id = 1, 
                    Name = "REINA CHLOE D. MAGPANTAY", 
                    Role = "Project Manager", 
                    PersonalGmail = "sample@gmail.com", 
                    SchoolGmail = "sample@gmail.com",
                    GithubUsername = "sample"
                },
                new TeamMember 
                { 
                    Id = 2, 
                    Name = "JOHANNA ANGELA P. ESTALILLA", 
                    Role = "Back-end Developer", 
                    PersonalGmail = "sample@gmail.com", 
                    SchoolGmail = "sample@gmail.com",
                    GithubUsername = "sample"
                },
                new TeamMember 
                { 
                    Id = 3, 
                    Name = "SARAH MAE D.C. HARINA", 
                    Role = "Front-end Developer", 
                    PersonalGmail = "sample@gmail.com", 
                    SchoolGmail = "sample@gmail.com",
                    GithubUsername = "sample"
                },
                new TeamMember 
                { 
                    Id = 4, 
                    Name = "JOSIAH ZACHARY Q. SY", 
                    Role = "Front-end Developer", 
                    PersonalGmail = "sample@gmail.com", 
                    SchoolGmail = "sample@gmail.com",
                    GithubUsername = "sample"
                },
            };

            return View(Team);
        }
    }
}
