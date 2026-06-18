using Microsoft.AspNetCore.Mvc;

namespace TETHER.Models
{
    public class TaskItem : Controller
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Priority { get; set; } = "High";      // High, Medium, Low
        public string Status { get; set; } = "Assigned";    // Assigned, Progress, Approval, Done
    }
}
