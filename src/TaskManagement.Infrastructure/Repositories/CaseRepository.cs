using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interfaces;
using TaskManagement.Infrastructure.Persistence;

namespace TaskManagement.Infrastructure.Repositories;

public class CaseRepository : Repository<Case>, ICaseRepository
{
    public CaseRepository(AppDbContext context) : base(context) { }
}
