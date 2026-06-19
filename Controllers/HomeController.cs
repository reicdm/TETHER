using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TETHER.Models;

namespace TETHER.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Login(string role)
        {
            return View();
        }

        public IActionResult Dashboard(string role)
        {
            ViewBag.Role = string.IsNullOrEmpty(role) ? "Member" : role;

            var dummyTask = new List<TaskItem>();

            // ASSIGNED
            for (int i = 1; i <= 3; i++)
                dummyTask.Add(new TaskItem { Id = i, Title = "Sample Title", Status = "Assigned" });

            // PENDING
            for (int i = 4; i <= 9; i++)
                dummyTask.Add(new TaskItem { Id = i, Title = "Sample Title", Status = "Pending" });

            // IN PROGRESS
            for (int i = 1; i <= 2; i++)
                dummyTask.Add(new TaskItem { Id = i + 9, Title = "Sample Title", Status = "Progress" });

            // DONE
            for (int i = 1; i <= 10; i++)
                dummyTask.Add(new TaskItem { Id = i + 11, Title = "Sample Title", Status = "Done" });

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
