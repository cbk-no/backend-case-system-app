using TaskManagement.Application.DTOs;

namespace TaskManagement.Application.Interfaces;

public interface ITaskService
{
    Task<TaskDto> CreateAsync(CreateTaskRequest request, CancellationToken ct = default);
    Task<TaskDto?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IReadOnlyList<TaskDto>> GetAllAsync(CancellationToken ct = default);
    Task<TaskDto?> UpdateAsync(Guid id, UpdateTaskRequest request, CancellationToken ct = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken ct = default);
    Task<IReadOnlyList<TaskDto>> GetByCaseAsync(Guid caseId, CancellationToken ct = default);
}
