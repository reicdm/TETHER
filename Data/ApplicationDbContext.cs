using Microsoft.EntityFrameworkCore;
using TETHER.Models.Entities;

namespace TETHER.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { 
        }

        public DbSet<Auth> Auths { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<TeamMemberRole> TeamMemberRoles { get; set; }
        public DbSet<TaskItem> TaskItems { get; set; }
        public DbSet<TaskItemAssignment> TaskItemAssignments { get; set; }
        public DbSet<PriorityLevel> PriorityLevels { get; set; }
        public DbSet<Status> Statuses { get; set; }
    }
}
