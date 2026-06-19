using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;

namespace TETHER.Models.Entities
{
    public class TaskItem : Controller
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public string PriorityLevelId { get; set; }
        public PriorityLevel? PriorityLevel { get; set; }

        public string Deadline { get; set; }
        public string DocsLink { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public string StatusId { get; set; }
        public Status? Status { get; set; }

        public int PmId { get; set; }
        public TeamMember? ProjectManager { get; set; }

        public ICollection<TaskItemAssignment> Assignments { get; set; }
            = new List<TaskItemAssignment>();
    }
}
