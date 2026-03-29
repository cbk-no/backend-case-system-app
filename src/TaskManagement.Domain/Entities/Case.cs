namespace TaskManagement.Domain.Entities;

public enum CaseStatus
{
    Open,
    InProgress,
    Closed
}

public enum CasePriority
{
    Low,
    Medium,
    High
}
public enum CaseType
{
    Complaint,
    Inquiry,
    Request
}

public class Case
{
    public Guid Id { get; set; }
    public DateTime DateReceived { get; set; }
    public DateTime Deadline { get; set; }
    public string Title { get; set; } = default!;
    public CaseType Type { get; set; }
    public string ComplaintDescription { get; set; } = default!;
    public CasePriority Priority { get; set; }
    public CaseStatus Status { get; set; }
    public string Description { get; set; } = default!;
    public string EmailComplainer { get; set; } = default!;
    public string UserInfoComplainer { get; set; } = default!;
    public Guid CaseOwnerId { get; set; }

        // Navigation
    public User CaseOwner { get; set; } = default!;
    public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
}
