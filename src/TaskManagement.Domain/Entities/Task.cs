namespace TaskManagement.Domain.Entities;

public enum CurrentStatus
{
    Todo,
    InProgress,
    Done
}
public class TaskItem
{
    public Guid Id { get; set; }
    public Guid AssignedUserId { get; set; }
    public string Description { get; set; } = default!;
    public CurrentStatus Status { get; set; } = default!;
    public Guid CaseId { get; set; }

    // Navigation
    public User AssignedUser { get; set; } = default!;
    public Case Case { get; set; } = default!;
}
