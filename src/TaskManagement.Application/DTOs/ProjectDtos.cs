namespace TaskManagement.Application.DTOs;

public record ProjectDto(Guid Id, string Name, Guid OwnerId);
public record CreateProjectRequest(string Name, Guid OwnerId);
public record UpdateProjectRequest(string Name);
