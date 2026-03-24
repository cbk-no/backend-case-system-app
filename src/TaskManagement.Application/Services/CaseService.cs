using AutoMapper;
using TaskManagement.Application.DTOs;
using TaskManagement.Application.Interfaces;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interfaces;

namespace TaskManagement.Application.Services;

public class CaseService : ICaseService
{
    private readonly ICaseRepository _repo;
    private readonly IProjectRepository _projectRepo;
    private readonly IUserRepository _userRepo;
    private readonly IMapper _mapper;

    public CaseService(
        ICaseRepository repo,
        IProjectRepository projectRepo,
        IUserRepository userRepo,
        IMapper mapper)
    {
        _repo = repo;
        _projectRepo = projectRepo;
        _userRepo = userRepo;
        _mapper = mapper;
    }

    public async Task<CaseDto> CreateAsync(CreateCaseRequest request, CancellationToken ct = default)
    {
        var project = await _projectRepo.GetByIdAsync(request.ProjectId, ct);
        if (project is null)
            throw new InvalidOperationException("Project not found");

        if (request.AssignedUserId.HasValue)
        {
            var user = await _userRepo.GetByIdAsync(request.AssignedUserId.Value, ct);
            if (user is null)
                throw new InvalidOperationException("Assigned user not found");
        }

        var task = new CaseItem
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Description = request.Description,
            Status = request.Status,
            ProjectId = request.ProjectId,
            AssignedUserId = request.AssignedUserId
        };

        await _repo.AddAsync(task, ct);
        return _mapper.Map<CaseDto>(task);
    }

    public async Task<CaseDto?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var task = await _repo.GetByIdAsync(id, ct);
        return task is null ? null : _mapper.Map<CaseDto>(task);
    }

    public async Task<IReadOnlyList<CaseDto>> GetAllAsync(CancellationToken ct = default)
    {
        var tasks = await _repo.GetAllAsync(ct);
        return _mapper.Map<IReadOnlyList<CaseDto>>(tasks);
    }

    public async Task<CaseDto?> UpdateAsync(Guid id, UpdateCaseRequest request, CancellationToken ct = default)
    {
        var task = await _repo.GetByIdAsync(id, ct);
        if (task is null) return null;

        task.Title = request.Title;
        task.Description = request.Description;
        task.Status = request.Status;
        task.AssignedUserId = request.AssignedUserId;

        await _repo.UpdateAsync(task, ct);
        return _mapper.Map<CaseDto>(task);
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var task = await _repo.GetByIdAsync(id, ct);
        if (task is null) return false;

        await _repo.DeleteAsync(task, ct);
        return true;
    }

    public async Task<CaseDto?> AssignAsync(Guid taskId, Guid userId, CancellationToken ct = default)
    {
        var task = await _repo.GetByIdAsync(taskId, ct);
        if (task is null) return null;

        var user = await _userRepo.GetByIdAsync(userId, ct);
        if (user is null)
            throw new InvalidOperationException("User not found");

        task.AssignedUserId = userId;
        await _repo.UpdateAsync(task, ct);

        return _mapper.Map<CaseDto>(task);
    }

    public async Task<IReadOnlyList<CaseDto>> GetByProjectAsync(Guid projectId, CancellationToken ct = default)
    {
        var tasks = await _repo.GetByProjectIdAsync(projectId, ct);
        return _mapper.Map<IReadOnlyList<CaseDto>>(tasks);
    }
}
