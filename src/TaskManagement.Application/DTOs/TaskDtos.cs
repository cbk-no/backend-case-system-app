namespace TaskManagement.Application.DTOs;

public record TaskDto(
    Guid Id,
    Guid AssignedUserId,
    string Description,
    string Status,
    Guid CaseId
);

public record CreateTaskRequest(
    Guid AssignedUserId,
    string Description,
    string Status,
    Guid CaseId
);
public record UpdateTaskRequest(
    Guid AssignedUserId,
    string Description,
    string Status,
    Guid CaseId
);
