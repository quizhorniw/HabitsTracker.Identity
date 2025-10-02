using Microsoft.EntityFrameworkCore;
using SevSU.HabitsTracker.Identity.Api.Models.Entities;

namespace SevSU.HabitsTracker.Identity.Api.DbContexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .ToTable("users")
            .HasKey(u => u.Id);
        modelBuilder.Entity<User>().Property(u => u.Id).ValueGeneratedOnAdd();
        
        modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
        modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();
        
        modelBuilder.Entity<User>().Property(u => u.Email).IsRequired().HasMaxLength(100);
        modelBuilder.Entity<User>().Property(u => u.Username).IsRequired().HasMaxLength(80);
        modelBuilder.Entity<User>().Property(u => u.PasswordHash).IsRequired().HasMaxLength(1000);
        modelBuilder.Entity<User>().Property(u => u.Role).IsRequired().HasMaxLength(50).HasDefaultValue("User");
        
        base.OnModelCreating(modelBuilder);
    }
}