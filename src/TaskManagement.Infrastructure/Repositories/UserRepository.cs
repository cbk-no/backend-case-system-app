using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interfaces;
using TaskManagement.Infrastructure.Persistence;

namespace TaskManagement.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _db;

    public UserRepository(AppDbContext db) => _db = db;

    public async Task AddAsync(User entity, CancellationToken ct = default)
    {
        await _db.Users.AddAsync(entity, ct);
        await _db.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(User entity, CancellationToken ct = default)
    {
        _db.Users.Remove(entity);
        await _db.SaveChangesAsync(ct);
    }

    public async Task<IReadOnlyList<User>> GetAllAsync(CancellationToken ct = default)
        => await _db.Users.AsNoTracking().ToListAsync(ct);

    public async Task<User?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => await _db.Users.FindAsync(new object?[] { id }, ct);

    public async Task<IReadOnlyList<User>> FindAsync(Expression<Func<User, bool>> predicate, CancellationToken ct = default)
        => await _db.Users.Where(predicate).AsNoTracking().ToListAsync(ct);

    public async Task UpdateAsync(User entity, CancellationToken ct = default)
    {
        _db.Users.Update(entity);
        await _db.SaveChangesAsync(ct);
    }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken ct = default)
        => await _db.Users.FirstOrDefaultAsync(x => x.Email == email, ct);
}
