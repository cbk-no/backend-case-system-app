namespace TaskManagement.Domain.Entities;

public enum CaseStatus
{
    Todo,
    InProgress,
    Done
}

public class CaseItem
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public CaseStatus Status { get; set; }

    public Guid ProjectId { get; set; }
    public Project Project { get; set; } = default!;

    public Guid? AssignedUserId { get; set; }
    public User? AssignedUser { get; set; }
}
