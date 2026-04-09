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
    private readonly IUserRepository _userRepo;

    public CaseService(ICaseRepository repo, IMapper mapper, IUserRepository userRepo)
    {
        _repo = repo;
        _mapper = mapper;
        _userRepo = userRepo;
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
        Console.WriteLine($"Before save: entity = {System.Text.Json.JsonSerializer.Serialize(entity)}");
        if (entity is null)
            return null;

        // ⭐ Merge only changed fields into the existing entity
        _mapper.Map(request, entity);
        if (request.CaseOwnerId.HasValue && request.CaseOwnerId.Value != Guid.Empty)
        {
            var exists = await _userRepo.ExistsAsync(request.CaseOwnerId.Value, ct);
            if (!exists)
                throw new Exception("Invalid CaseOwnerId: user does not exist");
            entity.CaseOwnerId = request.CaseOwnerId.Value;
        }
        Console.WriteLine($"Before save: CaseOwnerId = {entity.CaseOwnerId}");
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
