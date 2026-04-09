using TaskManagement.Domain.Entities;

namespace TaskManagement.Domain.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<bool> ExistsAsync(Guid id, CancellationToken ct = default);

    
}