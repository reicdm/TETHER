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

            // IN PROGRESS
            for (int i = 4; i <= 9; i++)
                dummyTask.Add(new TaskItem { Id = i, Title = "Sample Title", Status = "Progress" });

            // FOR APPROVAL
            for (int i = 1; i <= 2; i++)
                dummyTask.Add(new TaskItem { Id = i + 9, Title = "Sample Title", Status = "Approval" });

            // DONE
            for (int i = 1; i <= 10; i++)
                dummyTask.Add(new TaskItem { Id = i + 11, Title = "Sample Title", Status = "Done" });

            return View(dummyTask);
        }

        public IActionResult AddTask()
        {
            return View();
        }

    }
}
