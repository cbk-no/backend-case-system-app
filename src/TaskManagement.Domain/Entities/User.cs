namespace TaskManagement.Domain.Entities;

public enum Role
{
    Admin,
    User
}

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public Role Role { get; set; } = default!;

 // Navigation
    public ICollection<Case> OwnedCases { get; set; } = new List<Case>();
    public ICollection<TaskItem> AssignedTasks { get; set; } = new List<TaskItem>();
}
