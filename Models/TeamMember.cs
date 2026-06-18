using Microsoft.AspNetCore.Mvc;

namespace TETHER.Models
{
    public class TeamMember : Controller
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Figma { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
    }
}
