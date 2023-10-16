using Microsoft.EntityFrameworkCore;
using SchoolHubApi.Domain.Entities;

namespace SchoolHubApi.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<UserData> Users { get; set; }
    public DbSet<Pupil> Pupils { get; set; }
    public DbSet<Parent> Parents { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserData>()
            .Property(x => x.Role)
            .HasConversion<string>();
        
        modelBuilder.Entity<Parent>()
            .HasMany(p => p.Children)
            .WithMany(c => c.Parents)
            .UsingEntity(
                "ParentPupil",
                l => l.HasOne(typeof(Pupil)).WithMany().OnDelete(DeleteBehavior.NoAction),
                r => r.HasOne(typeof(Parent)).WithMany().OnDelete(DeleteBehavior.NoAction));
    }
}