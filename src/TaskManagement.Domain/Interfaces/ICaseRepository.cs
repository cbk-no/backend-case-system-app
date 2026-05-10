using TaskManagement.Domain.Entities;

namespace TaskManagement.Domain.Interfaces;

public interface ICaseRepository : IRepository<Case>
{
    Task<bool> ExistsAsync(Guid id, CancellationToken ct = default);
}
