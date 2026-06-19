using Microsoft.AspNetCore.Mvc;

namespace TETHER.Models
{
    public class CalendarEntry
    {
        public DateTime Date { get; set; }
        public string Color { get; set; } = "#cccccc";
        public string Highlight { get; set; } = "";
        public int Stars { get; set; }
        public string Emoji { get; set; } = "";
    }
}