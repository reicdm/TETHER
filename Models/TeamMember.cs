using Microsoft.AspNetCore.Mvc;

namespace TETHER.Models
{
    public class TeamMember
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Figma { get; set; } = string.Empty;
        public string? ProfileImageUrl { get; set; } = "/images/member-image/default.jpg";
    }
}
