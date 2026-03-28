using AutoMapper;
using TaskManagement.Application.DTOs;
using TaskManagement.Application.Interfaces;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interfaces;

namespace TaskManagement.Application.Services;

public class CaseService : ICaseService
{
    private readonly ICaseRepository _repo;
    private readonly IMapper _mapper;

    public CaseService(ICaseRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<CaseDto> CreateAsync(CreateCaseRequest request, CancellationToken ct = default)
    {
        var entity = _mapper.Map<Case>(request);
        await _repo.AddAsync(entity, ct);
        return _mapper.Map<CaseDto>(entity);
    }

    public async Task<CaseDto?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var entity = await _repo.GetByIdAsync(id, ct);
        return entity is null ? null : _mapper.Map<CaseDto>(entity);
    }

    public async Task<IReadOnlyList<CaseDto>> GetAllAsync(CancellationToken ct = default)
    {
        var cases = await _repo.GetAllAsync(ct);
        return _mapper.Map<IReadOnlyList<CaseDto>>(cases);
    }

    public async Task<CaseDto?> UpdateAsync(Guid id, UpdateCaseRequest request, CancellationToken ct = default)
    {
        var entity = await _repo.GetByIdAsync(id, ct);
        if (entity is null) return null;
        _mapper.Map(request, entity);
        await _repo.UpdateAsync(entity, ct);
        return _mapper.Map<CaseDto>(entity);
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var entity = await _repo.GetByIdAsync(id, ct);
        if (entity is null) return false;
        await _repo.DeleteAsync(entity, ct);
        return true;
    }

    public async Task<CaseDto?> AssignAsync(Guid caseId, Guid userId, CancellationToken ct = default)
    {
        var entity = await _repo.GetByIdAsync(caseId, ct);
        if (entity is null) return null;
        entity.CaseOwnerId = userId;
        await _repo.UpdateAsync(entity, ct);
        return _mapper.Map<CaseDto>(entity);
    }

    public async Task<IReadOnlyList<CaseDto>> GetByProjectAsync(Guid projectId, CancellationToken ct = default)
    {
        var all = await _repo.GetAllAsync(ct);
        return _mapper.Map<IReadOnlyList<CaseDto>>(all);
    }
}
