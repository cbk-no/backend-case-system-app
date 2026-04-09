using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Infrastructure.Persistence;

namespace TaskManagement.Infrastructure.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken ct = default)
    {
        return await _context.Users.AnyAsync(u => u.Id == id, ct);
    }
}
