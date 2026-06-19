using Microsoft.EntityFrameworkCore;
using TETHER.Models.Entities;

namespace TETHER.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { 
        }
        public DbSet<TaskItem> Tasks { get; set;  }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskItemAssignment>()
                .HasKey(t => new { t.TaskId, t.AssignedTo });
        }
    }
}
