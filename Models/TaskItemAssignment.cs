namespace TETHER.Models
{
    public class TaskItemAssignment
    {
        public int TaskId { get; set; }
        public TaskItem? Task { get; set; }

        public int AssignedTo { get; set; }
        public TeamMember? TeamMember { get; set; }

        public DateTime AssignedAt { get; set; }
    }
}
