using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolHubApi.Models.Domain;
using System.Diagnostics.CodeAnalysis;

namespace SchoolHubApi.Data
{
    public class ApplicationDbContext : DbContext
    {
            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                : base(options){}
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<ParentChild> Parentchildren { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Startup> Classes { get; set; }
        public DbSet<CourseClass> CourseClasses { get; set; }
        public DbSet<Homework> Homeworks { get; set; }
        public DbSet<Pupil> Pupils { get; set; }
        public DbSet<HomeworkPupil> HomeworkPupils { get; set; }
        public DbSet<ClassCoursePupilGrade> ClassCoursePupilGrades { get; set; }
        public DbSet<GradeName> GradeNames { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ParentChild>()
             .HasOne(pc => pc.Child)
             .WithMany(u => u.Children)
             .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ParentChild>()
                .HasOne(pc => pc.Parent)
                .WithMany(u => u.Parents)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<HomeworkPupil>()
                .HasOne(hp => hp.Pupil)
                .WithMany(p => p.HomeworkPupils)
                .OnDelete(DeleteBehavior.NoAction);

            base.OnModelCreating(modelBuilder);
        }
    }
}
