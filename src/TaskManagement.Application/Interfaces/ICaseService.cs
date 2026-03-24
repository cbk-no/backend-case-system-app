using TaskManagement.Application.DTOs;

namespace TaskManagement.Application.Interfaces;

public interface ICaseService
{
    Task<CaseDto> CreateAsync(CreateCaseRequest request, CancellationToken ct = default);
    Task<CaseDto?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IReadOnlyList<CaseDto>> GetAllAsync(CancellationToken ct = default);
    Task<CaseDto?> UpdateAsync(Guid id, UpdateCaseRequest request, CancellationToken ct = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken ct = default);
    Task<CaseDto?> AssignAsync(Guid taskId, Guid userId, CancellationToken ct = default);
    Task<IReadOnlyList<CaseDto>> GetByProjectAsync(Guid projectId, CancellationToken ct = default);
}
