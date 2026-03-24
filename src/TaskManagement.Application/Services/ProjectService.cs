using AutoMapper;
using TaskManagement.Application.DTOs;
using TaskManagement.Application.Interfaces;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interfaces;

namespace TaskManagement.Application.Services;

public class ProjectService : IProjectService
{
    private readonly IProjectRepository _repo;
    private readonly IUserRepository _userRepo;
    private readonly IMapper _mapper;

    public ProjectService(IProjectRepository repo, IUserRepository userRepo, IMapper mapper)
    {
        _repo = repo;
        _userRepo = userRepo;
        _mapper = mapper;
    }

    public async Task<ProjectDto> CreateAsync(CreateProjectRequest request, CancellationToken ct = default)
    {
        var owner = await _userRepo.GetByIdAsync(request.OwnerId, ct);
        if (owner is null)
            throw new InvalidOperationException("Owner not found");

        var project = new Project
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            OwnerId = request.OwnerId
        };

        await _repo.AddAsync(project, ct);
        return _mapper.Map<ProjectDto>(project);
    }

    public async Task<ProjectDto?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var project = await _repo.GetByIdAsync(id, ct);
        return project is null ? null : _mapper.Map<ProjectDto>(project);
    }

    public async Task<IReadOnlyList<ProjectDto>> GetAllAsync(CancellationToken ct = default)
    {
        var projects = await _repo.GetAllAsync(ct);
        return _mapper.Map<IReadOnlyList<ProjectDto>>(projects);
    }

    public async Task<ProjectDto?> UpdateAsync(Guid id, UpdateProjectRequest request, CancellationToken ct = default)
    {
        var project = await _repo.GetByIdAsync(id, ct);
        if (project is null) return null;

        project.Name = request.Name;
        await _repo.UpdateAsync(project, ct);

        return _mapper.Map<ProjectDto>(project);
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var project = await _repo.GetByIdAsync(id, ct);
        if (project is null) return false;

        await _repo.DeleteAsync(project, ct);
        return true;
    }
}
