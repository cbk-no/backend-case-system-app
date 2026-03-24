namespace TaskManagement.Domain.Entities;

public class Project
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public Guid OwnerId { get; set; }

    public User Owner { get; set; } = default!;
    public ICollection<CaseItem> Tasks { get; set; } = new List<CaseItem>();
}
