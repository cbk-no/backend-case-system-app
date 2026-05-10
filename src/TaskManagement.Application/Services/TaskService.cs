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
    private readonly IUserRepository _userRepo;
    private readonly ICaseRepository _caseRepo;

    public TaskService(ITaskRepository repo, IMapper mapper, IUserRepository userRepo, ICaseRepository caseRepo)
    {
        _repo = repo;
        _mapper = mapper;
        _userRepo = userRepo;
        _caseRepo = caseRepo;
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
        Console.WriteLine($"Before save: entity = {System.Text.Json.JsonSerializer.Serialize(entity)}");
        _mapper.Map(request, entity);

        // Status
        if (!string.IsNullOrWhiteSpace(request.Status))
        {
            entity.Status = Enum.Parse<CurrentStatus>(request.Status, ignoreCase: true);
        }

        // AssignedUserId
        if (request.AssignedUserId.HasValue && request.AssignedUserId.Value != Guid.Empty)
        {
            var exists = await _userRepo.ExistsAsync(request.AssignedUserId.Value, ct);
            if (!exists)
                throw new Exception("Invalid AssignedUserId: user does not exist");

            entity.AssignedUserId = request.AssignedUserId.Value;
        }
        if (request.CaseId.HasValue && request.CaseId.Value != Guid.Empty)
        {
            var caseExists = await _caseRepo.ExistsAsync(request.CaseId.Value, ct);
            if (!caseExists)
                throw new Exception("Invalid CaseId: case does not exist");

            entity.CaseId = request.CaseId.Value;
        }

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
