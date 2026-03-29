using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<TaskItem> Tasks => Set<TaskItem>();
    public DbSet<Case> Cases => Set<Case>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // User
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id);
            entity.Property(u => u.Name).IsRequired().HasMaxLength(200);
            entity.Property(u => u.Role).IsRequired().HasMaxLength(100);
            entity.Property(u => u.Email).IsRequired().HasMaxLength(200);
        });

        // Case
        modelBuilder.Entity<Case>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.DateReceived).IsRequired();
            entity.Property(c => c.Deadline).IsRequired();
            entity.Property(c => c.Title).IsRequired().HasMaxLength(200);
            entity.Property(c => c.Type).IsRequired();
            entity.Property(c => c.ComplaintDescription).IsRequired();
            entity.Property(c => c.Priority).IsRequired();
            entity.Property(c => c.Status).IsRequired();
            entity.Property(c => c.Description).IsRequired();
            entity.Property(c => c.EmailComplainer).IsRequired();
            entity.Property(c => c.UserInfoComplainer).IsRequired();

            entity.HasOne(c => c.CaseOwner)
                  .WithMany(u => u.OwnedCases)
                  .HasForeignKey(c => c.CaseOwnerId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // TaskItem
        modelBuilder.Entity<TaskItem>(entity =>
        {
            entity.HasKey(t => t.Id);

            entity.Property(t => t.Description).IsRequired();
            entity.Property(t => t.Status).IsRequired();

            entity.HasOne(t => t.AssignedUser)
                  .WithMany(u => u.AssignedTasks)
                  .HasForeignKey(t => t.AssignedUserId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(t => t.Case)
                  .WithMany(c => c.Tasks)
                  .HasForeignKey(t => t.CaseId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
