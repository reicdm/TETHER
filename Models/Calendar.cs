using Microsoft.AspNetCore.Mvc;

namespace TETHER.Models
{
    public class CalendarViewModel
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public string MonthName => new DateTime(Year, Month, 1).ToString("MMMM yyyy");
        public DateTime Prev { get; set; }
        public DateTime Next { get; set; }
        public int DaysInMonth { get; set; }
        public int FirstDayOfWeek { get; set; }
        public Dictionary<int, CalendarEntry> Entries { get; set; } = new();
    }
}