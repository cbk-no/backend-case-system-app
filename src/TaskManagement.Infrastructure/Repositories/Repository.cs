using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain.Interfaces;
using TaskManagement.Infrastructure.Persistence;

namespace TaskManagement.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly AppDbContext _context;

    public Repository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken ct = default) =>
        await _context.Set<T>().FindAsync(id);

    public async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken ct = default) =>
        await _context.Set<T>().ToListAsync(ct);

    public async Task AddAsync(T entity, CancellationToken ct = default)
    {
        _context.Set<T>().Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity, CancellationToken ct = default)
    {
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity, CancellationToken ct = default)
    {
        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync();
    }
}
