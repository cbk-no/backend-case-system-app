using AutoMapper;
using TaskManagement.Application.DTOs;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interfaces;
using TaskManagement.Application.Interfaces;

namespace TaskManagement.Application.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _repo;
    private readonly IMapper _mapper;

    public TaskService(ITaskRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<TaskDto> CreateAsync(CreateTaskRequest request, CancellationToken ct = default)
    {
        var entity = _mapper.Map<TaskItem>(request);
        await _repo.AddAsync(entity, ct);
        return _mapper.Map<TaskDto>(entity);
    }

    public async Task<TaskDto?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var entity = await _repo.GetByIdAsync(id, ct);
        return entity is null ? null : _mapper.Map<TaskDto>(entity);
    }

    public async Task<IReadOnlyList<TaskDto>> GetAllAsync(CancellationToken ct = default)
    {
        var tasks = await _repo.GetAllAsync(ct);
        return _mapper.Map<IReadOnlyList<TaskDto>>(tasks);
    }

    public async Task<TaskDto?> UpdateAsync(Guid id, UpdateTaskRequest request, CancellationToken ct = default)
    {
        var entity = await _repo.GetByIdAsync(id, ct);
        if (entity is null) return null;
        _mapper.Map(request, entity);
        await _repo.UpdateAsync(entity, ct);
        return _mapper.Map<TaskDto>(entity);
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var entity = await _repo.GetByIdAsync(id, ct);
        if (entity is null) return false;
        await _repo.DeleteAsync(entity, ct);
        return true;
    }

    public async Task<IReadOnlyList<TaskDto>> GetByCaseAsync(Guid caseId, CancellationToken ct = default)
    {
        var all = await _repo.GetAllAsync(ct);
        var filtered = all.Where(t => t.CaseId == caseId).ToList();
        return _mapper.Map<IReadOnlyList<TaskDto>>(filtered);
    }
}
