namespace TaskManagement.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;

    public ICollection<Project> Projects { get; set; } = new List<Project>();
    public ICollection<CaseItem> AssignedTasks { get; set; } = new List<CaseItem>();
}
