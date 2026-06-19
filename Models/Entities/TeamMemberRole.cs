namespace TETHER.Models.Entities
{
    public class TeamMemberRole
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public ICollection<TeamMember> TeamMembers { get; set; } 
            = new List<TeamMember>();
    }
}
