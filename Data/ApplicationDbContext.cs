using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SchoolHubApi.Domain.Entities;
using SchoolHubApi.Domain.Entities.Enums;
using SchoolHubApi.Helpers;
using SchoolHubApi.Models.EmailDto;

namespace SchoolHubApi.Data;

public class ApplicationDbContext : DbContext
{
    private readonly EmailSettings _mailSettings;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
       IOptions<EmailSettings> mailSettings)
        : base(options)
    {
        _mailSettings = mailSettings.Value;
    }

    public DbSet<UserData> Users { get; set; }
    public DbSet<Pupil> Pupils { get; set; }
    public DbSet<Parent> Parents { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<ResetPasswordCode> ResetPasswordCodes { get; set; }
    public DbSet<Classroom> Classrooms { get; set; }    
    public DbSet<Course> Courses { get; set; }
    public DbSet<Topic> Topics { get; set; }
    public DbSet<Homework> Homeworks { get; set; }
    public DbSet<Mark> Marks { get; set; }
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserData>()
            .Property(x => x.Role)
            .HasConversion<string>();

        modelBuilder.Entity<UserData>()
            .HasOne(x => x.ResetPasswordCode)
            .WithOne()
            .HasForeignKey<ResetPasswordCode>(x => x.Email);

        modelBuilder.Entity<Parent>()
            .HasMany(p => p.Children)
            .WithMany(c => c.Parents)
            .UsingEntity(
                "ParentPupil",
                l => l.HasOne(typeof(Pupil)).WithMany().OnDelete(DeleteBehavior.NoAction),
                r => r.HasOne(typeof(Parent)).WithMany().OnDelete(DeleteBehavior.NoAction));


        modelBuilder.Entity<Pupil>()
            .HasIndex(x => x.AccessCode)
            .IsUnique();

        modelBuilder.Entity<Classroom>()
            .HasIndex(x => x.ClassAccessCode)
            .IsUnique();

        modelBuilder.Entity<Mark>()
            .HasOne(m => m.Homework)
            .WithOne(h => h.Mark)
            .HasForeignKey<Mark>(x =>x.HomeworkId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Homework>()
            .HasOne(m => m.Pupil)
            .WithMany(h => h.Homeworks)
            .HasForeignKey(x => x.PupilId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Homework>()
            .HasOne(h => h.Topic)
            .WithMany(t => t.Homeworks)
            .HasForeignKey(h => h.TopicId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<Mark>()
            .HasOne(m => m.Pupil)
            .WithMany(p => p.Marks)
            .HasForeignKey(m => m.PupilId)
            .OnDelete(DeleteBehavior.NoAction);



        HashPasswordHelper.CreatePasswordHash("admin", out var passwordHash, out var passwordSalt);

        modelBuilder.Entity<UserData>()
            .HasData(new UserData()
            {
                Email = _mailSettings.AdminMail,
                FirstName = "Admin",
                LastName = "",
                PhoneNumber = "",
                Pesel = "",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Role = Role.Admin
            });
    }
}