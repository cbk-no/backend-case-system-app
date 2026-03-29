using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain.Entities;
using TaskManagement.Infrastructure.Persistence;

namespace TaskManagement.Infrastructure.Database;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(AppDbContext db)
    {
        if (await db.Users.AnyAsync())
            return;

        // --- Users ---
        var users = new List<User>
        {
            new() { Id = Guid.NewGuid(), Name = "Alice",   Role = Role.Admin, Email = "alice@example.com" },
            new() { Id = Guid.NewGuid(), Name = "Bob",     Role = Role.User,  Email = "bob@example.com" },
            new() { Id = Guid.NewGuid(), Name = "Charlie", Role = Role.User,  Email = "charlie@example.com" },
            new() { Id = Guid.NewGuid(), Name = "Diana",   Role = Role.Admin, Email = "diana@example.com" },
            new() { Id = Guid.NewGuid(), Name = "Eve",     Role = Role.User,  Email = "eve@example.com" }
        };

        db.Users.AddRange(users);

        // --- Cases ---
        var cases = new List<Case>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Title = "Noise Complaint",
                Type = CaseType.Complaint,
                DateReceived = DateTime.UtcNow.AddDays(-3),
                Deadline = DateTime.UtcNow.AddDays(4),
                ComplaintDescription = "Loud noise every night.",
                Priority = CasePriority.High,
                Status = CaseStatus.Open,
                Description = "Resident reports ongoing noise issues.",
                EmailComplainer = "resident1@example.com",
                UserInfoComplainer = "John Doe, Apt 12B",
                CaseOwnerId = users[0].Id
            },
            new()
            {
                Id = Guid.NewGuid(),
                Title = "Elevator Issue",
                Type = CaseType.Request,
                DateReceived = DateTime.UtcNow.AddDays(-1),
                Deadline = DateTime.UtcNow.AddDays(7),
                ComplaintDescription = "Elevator stops between floors.",
                Priority = CasePriority.Medium,
                Status = CaseStatus.InProgress,
                Description = "Elevator malfunction reported.",
                EmailComplainer = "tenant@example.com",
                UserInfoComplainer = "Sarah Lee, Apt 7A",
                CaseOwnerId = users[1].Id
            },
            new()
            {
                Id = Guid.NewGuid(),
                Title = "Water Leak",
                Type = CaseType.Complaint,
                DateReceived = DateTime.UtcNow.AddDays(-2),
                Deadline = DateTime.UtcNow.AddDays(2),
                ComplaintDescription = "Leak in bathroom ceiling.",
                Priority = CasePriority.High,
                Status = CaseStatus.Open,
                Description = "Water dripping from ceiling.",
                EmailComplainer = "resident2@example.com",
                UserInfoComplainer = "Mark Smith, Apt 3C",
                CaseOwnerId = users[2].Id
            },
            new()
            {
                Id = Guid.NewGuid(),
                Title = "Parking Inquiry",
                Type = CaseType.Inquiry,
                DateReceived = DateTime.UtcNow.AddDays(-5),
                Deadline = DateTime.UtcNow.AddDays(10),
                ComplaintDescription = "Question about parking availability.",
                Priority = CasePriority.Low,
                Status = CaseStatus.Closed,
                Description = "Resident asked about parking rules.",
                EmailComplainer = "resident3@example.com",
                UserInfoComplainer = "Anna Brown, Apt 9D",
                CaseOwnerId = users[3].Id
            },
            new()
            {
                Id = Guid.NewGuid(),
                Title = "Heating Issue",
                Type = CaseType.Request,
                DateReceived = DateTime.UtcNow.AddDays(-4),
                Deadline = DateTime.UtcNow.AddDays(3),
                ComplaintDescription = "Heating not working properly.",
                Priority = CasePriority.Medium,
                Status = CaseStatus.InProgress,
                Description = "Apartment temperature too low.",
                EmailComplainer = "resident4@example.com",
                UserInfoComplainer = "Tom White, Apt 5E",
                CaseOwnerId = users[4].Id
            }
        };

        db.Cases.AddRange(cases);

        // --- Tasks ---
        var tasks = new List<TaskItem>
        {
            new() { Id = Guid.NewGuid(), AssignedUserId = users[1].Id, Description = "Interview neighbors", Status = CurrentStatus.Todo,        CaseId = cases[0].Id },
            new() { Id = Guid.NewGuid(), AssignedUserId = users[2].Id, Description = "Inspect elevator",    Status = CurrentStatus.InProgress, CaseId = cases[1].Id },
            new() { Id = Guid.NewGuid(), AssignedUserId = users[3].Id, Description = "Check leak source",   Status = CurrentStatus.Todo,        CaseId = cases[2].Id },
            new() { Id = Guid.NewGuid(), AssignedUserId = users[4].Id, Description = "Respond to inquiry",  Status = CurrentStatus.Done,        CaseId = cases[3].Id },
            new() { Id = Guid.NewGuid(), AssignedUserId = users[0].Id, Description = "Inspect heating",     Status = CurrentStatus.InProgress, CaseId = cases[4].Id }
        };

        db.Tasks.AddRange(tasks);

        await db.SaveChangesAsync();
    }
}
