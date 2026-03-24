using TaskManagement.Domain.Entities;

namespace TaskManagement.Domain.Interfaces;

public interface ICaseRepository : IRepository<CaseItem>
{
    Task<IReadOnlyList<CaseItem>> GetByProjectIdAsync(Guid projectId, CancellationToken ct = default);
}
