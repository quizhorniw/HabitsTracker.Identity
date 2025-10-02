using Microsoft.EntityFrameworkCore;
using SevSU.HabitsTracker.Identity.Api.Models.Entities;

namespace SevSU.HabitsTracker.Identity.Api.DbContexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
    public DbSet<User> Users { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasKey(u => u.Id);
        modelBuilder.Entity<User>().Property(u => u.Id).ValueGeneratedOnAdd();
        
        modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
        modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();
        
        modelBuilder.Entity<User>().Property(u => u.Email).IsRequired().HasMaxLength(100);
        modelBuilder.Entity<User>().Property(u => u.Username).IsRequired().HasMaxLength(80);
        modelBuilder.Entity<User>().Property(u => u.PasswordHash).IsRequired().HasMaxLength(1000);
        modelBuilder.Entity<User>().Property(u => u.Role).IsRequired().HasMaxLength(50).HasDefaultValue("User");
        
        modelBuilder.Entity<RefreshToken>().HasKey(t => t.Id);
        modelBuilder.Entity<User>().Property(t => t.Id).ValueGeneratedOnAdd();

        modelBuilder.Entity<RefreshToken>().HasIndex(t => t.Token).IsUnique();
        modelBuilder.Entity<RefreshToken>().HasIndex(t => t.UserId);
        
        modelBuilder.Entity<RefreshToken>().Property(t => t.Token).IsRequired().HasMaxLength(512);
        modelBuilder.Entity<RefreshToken>().Property(t => t.UserId).IsRequired();
        
        base.OnModelCreating(modelBuilder);
    }
}