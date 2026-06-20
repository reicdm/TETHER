using System.ComponentModel.DataAnnotations;
using TETHER.Models.Entities;

namespace TETHER.Models.ViewModels
{
    public class TaskFormViewModel
    {
        [Required(ErrorMessage = "Task name is required.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please select a priority.")]
        public string SelectedPriority { get; set; } = string.Empty;

        [Required(ErrorMessage = "Start date is required.")]
        public DateTime? StartDate { get; set; }

        [Required(ErrorMessage = "Deadline is required.")]
        public DateTime? Deadline { get; set; }

        public string? DocsLink { get; set; }

        public string? Description { get; set; }

        public string SelectedStatus { get; set; } = "Pending";

        public List<int> SelectedMembers { get; set; } = new();

        public List<TeamMember>? AvailableMembers { get; set; }
    }
}