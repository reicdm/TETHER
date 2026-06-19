using Microsoft.EntityFrameworkCore;
using TETHER.Models.Entities;

namespace TETHER.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskItemAssignment>()
                .HasKey(t => new { t.TaskId, t.AssignedTo });
        }
        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<TeamMemberRole> TeamMemberRoles { get; set; }
        public DbSet<TaskItem> TaskItems { get; set; }
        public DbSet<TaskItemAssignment> TaskItemAssignments { get; set; }
        public DbSet<PriorityLevel> PriorityLevels { get; set; }
        public DbSet<Status> Statuses { get; set; }
    }
}
