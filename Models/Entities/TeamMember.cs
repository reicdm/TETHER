using Microsoft.AspNetCore.Mvc;

namespace TETHER.Models.Entities
{
    public class TeamMember
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string MiddleName { get; set; }
        public string LastName { get; set; } = string.Empty;

        public int RoleId { get; set; }
        public TeamMemberRole? Role { get; set; }

        public string PersonalGmail { get; set; } = string.Empty;
        public string SchoolGmail { get; set; } = string.Empty;
        public string GithubUsername { get; set; } = string.Empty;
        public string ProfileImageUrl { get; set; } = "/images/member-image/default.jpg";

        public int? PmId { get; set; }
        public TeamMember? ProjectManager { get; set; }
    }
}
