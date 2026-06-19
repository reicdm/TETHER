using Microsoft.EntityFrameworkCore;
using TETHER.Models.Entities;

namespace TETHER.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<TeamMemberRole> TeamMemberRoles { get; set; }
        public DbSet<TaskItem> TaskItems { get; set; }
        public DbSet<TaskItemAssignment> TaskItemAssignments { get; set; }
        public DbSet<PriorityLevel> PriorityLevels { get; set; }
        public DbSet<Status> Statuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskItemAssignment>()
                .HasKey(t => new { t.TaskId, t.AssignedTo });

            modelBuilder.Entity<TeamMemberRole>().HasData(
                new TeamMemberRole { Id = 1, Name = "Project Manager" },
                new TeamMemberRole { Id = 2, Name = "Backend Developer" },
                new TeamMemberRole { Id = 3, Name = "Frontend Developer" }
            );

            modelBuilder.Entity<PriorityLevel>().HasData(
                new PriorityLevel { Id = 1, Name = "Low" },
                new PriorityLevel { Id = 2, Name = "Medium" },
                new PriorityLevel { Id = 3, Name = "High" }
            );

            modelBuilder.Entity<Status>().HasData(
                new Status { Id = 1, Name = "Pending" },
                new Status { Id = 2, Name = "In Progress" },
                new Status { Id = 3, Name = "Completed" }
            );

            modelBuilder.Entity<TeamMember>().HasData(
                new TeamMember
                {
                    Id = 1,
                    FirstName = "Reina Chloe",
                    MiddleName = "De Roja",
                    LastName = "Magpantay",
                    RoleId = 1, // Project Manager
                    PersonalGmail = "rcdrmagpantay@gmail.com",
                    SchoolGmail = "reinachloedmagpantay@iskolarngbayan.pup.edu.ph",
                    GithubUsername = "reicdm",
                    Password = "pm012345",
                    ProfileImageUrl = "/images/member-image/rei.jpg",
                    PmId = null 
                },
                new TeamMember
                {
                    Id = 2,
                    FirstName = "Johanna Angela",
                    MiddleName = "Quilatan",
                    LastName = "Estalilla",
                    RoleId = 2, // Backend Developer
                    PersonalGmail = "pupbsitestalillajohanna@gmail.com",
                    SchoolGmail = "johannaangelapestalilla@iskolarngbayan.pup.edu.ph",
                    GithubUsername = "johannaestalilla1205",
                    Password = "member01",
                    ProfileImageUrl = "/images/member-image/hanna.jpg",
                    PmId = 1
                },
                new TeamMember
                {
                    Id = 3,
                    FirstName = "Sarah Mae",
                    MiddleName = "Dela Cruz",
                    LastName = "Harina",
                    RoleId = 3, // Frontend Developer
                    PersonalGmail = "sarahmaeharina@gmail.com",
                    SchoolGmail = "sarahmaedharina@iskolarngbayan.pup.edu.ph",
                    GithubUsername = "smhix",
                    Password = "member02",
                    ProfileImageUrl = "/images/member-image/sarah.jpg",
                    PmId = 1 
                },
                new TeamMember
                {
                    Id = 4,
                    FirstName = "Josiah Zachary",
                    MiddleName = "Quinones",
                    LastName = "Sy",
                    RoleId = 3, // Frontend Developer
                    PersonalGmail = "bsitsyjosiah@gmail.com",
                    SchoolGmail = "josiahzacharyqsy@iskolarngbayan.pup.edu.ph",
                    GithubUsername = "znacku",
                    Password = "member03",
                    ProfileImageUrl = "/images/member-image/zach.jpg",
                    PmId = 1 
                }
            );
        }
    }
}