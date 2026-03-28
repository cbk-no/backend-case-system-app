using AutoMapper;
using TaskManagement.Application.DTOs;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Case, CaseDto>();
        CreateMap<CreateCaseRequest, Case>();

        CreateMap<TaskItem, TaskDto>();
        CreateMap<CreateTaskRequest, TaskItem>();

        CreateMap<User, UserDto>();
        CreateMap<CreateUserRequest, User>();
    }
}
