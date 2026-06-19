using Microsoft.AspNetCore.Mvc;

namespace TETHER.Models
{
    public class CalendarEntry
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Priority { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string AssignedTo { get; set; } = string.Empty;
    }
}