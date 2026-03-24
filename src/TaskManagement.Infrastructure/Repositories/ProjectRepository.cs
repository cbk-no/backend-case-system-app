using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interfaces;
using TaskManagement.Infrastructure.Persistence;

namespace TaskManagement.Infrastructure.Repositories;

public class ProjectRepository : IProjectRepository
{
    private readonly AppDbContext _db;

    public ProjectRepository(AppDbContext db) => _db = db;

    public async Task AddAsync(Project entity, CancellationToken ct = default)
    {
        await _db.Projects.AddAsync(entity, ct);
        await _db.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Project entity, CancellationToken ct = default)
    {
        _db.Projects.Remove(entity);
        await _db.SaveChangesAsync(ct);
    }

    public async Task<IReadOnlyList<Project>> GetAllAsync(CancellationToken ct = default)
        => await _db.Projects.AsNoTracking().ToListAsync(ct);

    public async Task<Project?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => await _db.Projects.FindAsync(new object?[] { id }, ct);

    public async Task<IReadOnlyList<Project>> FindAsync(Expression<Func<Project, bool>> predicate, CancellationToken ct = default)
        => await _db.Projects.Where(predicate).AsNoTracking().ToListAsync(ct);

    public async Task UpdateAsync(Project entity, CancellationToken ct = default)
    {
        _db.Projects.Update(entity);
        await _db.SaveChangesAsync(ct);
    }
}
