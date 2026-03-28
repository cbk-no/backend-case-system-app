namespace TaskManagement.Application.DTOs;

public record UserDto(
    Guid Id,
    string Name,
    string Role,
    string Email
);

public record CreateUserRequest(
    string Name,
    string Role,
    string Email
);
public record UpdateUserRequest(
    string Name,
    string Role,
    string Email
);
