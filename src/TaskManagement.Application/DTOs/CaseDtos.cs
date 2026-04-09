using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.DTOs;

public record CaseDto(
    Guid Id,
    DateTime DateReceived,
    DateTime Deadline,
    string Title,
    string Type,
    string ComplaintDescription,
    string Priority,
    string Status,
    string Description,
    string EmailComplainer,
    string UserInfoComplainer,
    Guid CaseOwnerId
);

public record CreateCaseRequest(
    DateTime DateReceived,
    DateTime Deadline,
    string Title,
    string Type,
    string ComplaintDescription,
    string Priority,
    string Status,
    string Description,
    string EmailComplainer,
    string UserInfoComplainer,
    Guid CaseOwnerId
);

public record UpdateCaseRequest(
    DateTime? DateReceived,
    DateTime? Deadline,
    string? Title,
    string? Type,
    string? ComplaintDescription,
    string? Priority,
    string? Status,
    string? Description,
    string? EmailComplainer,
    string? UserInfoComplainer,
    Guid? CaseOwnerId
);


public record AssignCaseRequest(Guid UserId);
