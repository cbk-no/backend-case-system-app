using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interfaces;
using TaskManagement.Infrastructure.Persistence;

namespace TaskManagement.Infrastructure.Repositories;

public class TaskRepository : ICaseRepository
{
    private readonly AppDbContext _db;

    public TaskRepository(AppDbContext db) => _db = db;

    public async Task AddAsync(CaseItem entity, CancellationToken ct = default)
    {
        await _db.Tasks.AddAsync(entity, ct);
        await _db.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(CaseItem entity, CancellationToken ct = default)
    {
        _db.Tasks.Remove(entity);
        await _db.SaveChangesAsync(ct);
    }

    public async Task<IReadOnlyList<CaseItem>> GetAllAsync(CancellationToken ct = default)
        => await _db.Tasks.AsNoTracking().ToListAsync(ct);

    public async Task<CaseItem?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => await _db.Tasks.FindAsync(new object?[] { id }, ct);

    public async Task<IReadOnlyList<CaseItem>> FindAsync(Expression<Func<CaseItem, bool>> predicate, CancellationToken ct = default)
        => await _db.Tasks.Where(predicate).AsNoTracking().ToListAsync(ct);

    public async Task UpdateAsync(CaseItem entity, CancellationToken ct = default)
    {
        _db.Tasks.Update(entity);
        await _db.SaveChangesAsync(ct);
    }

    public async Task<IReadOnlyList<CaseItem>> GetByProjectIdAsync(Guid projectId, CancellationToken ct = default)
        => await _db.Tasks
            .Where(t => t.ProjectId == projectId)
            .AsNoTracking()
            .ToListAsync(ct);
}
