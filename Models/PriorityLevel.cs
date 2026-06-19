namespace TETHER.Models
{
    public class PriorityLevel
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public ICollection<TaskItem> Tasks { get; set; }
            = new List<TaskItem>();
    }
}
