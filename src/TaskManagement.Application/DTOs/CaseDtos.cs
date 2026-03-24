using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.DTOs;

public record CaseDto(
    Guid Id,
    string Title,
    string? Description,
    CaseStatus Status,
    Guid ProjectId,
    Guid? AssignedUserId);

public record CreateCaseRequest(
    string Title,
    string? Description,
    CaseStatus Status,
    Guid ProjectId,
    Guid? AssignedUserId);

public record UpdateCaseRequest(
    string Title,
    string? Description,
    CaseStatus Status,
    Guid? AssignedUserId);

public record AssignCaseRequest(Guid UserId);
