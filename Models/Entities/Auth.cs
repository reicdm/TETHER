namespace TETHER.Models.Entities
{
    public class Auth
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;

        public int TeamMemberId { get; set; }
        public TeamMember? TeamMember { get; set; }
    }
}
