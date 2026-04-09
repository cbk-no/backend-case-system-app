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
        CreateMap<UpdateCaseRequest, Case>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Tasks, opt => opt.Ignore())
            .ForMember(dest => dest.CaseOwner, opt => opt.Ignore())
            .ForMember(dest => dest.CaseOwnerId, opt => opt.Ignore())
            .ForAllMembers(opt =>
                opt.Condition((src, dest, srcValue) => srcValue != null));
        CreateMap<TaskItem, TaskDto>();
        CreateMap<CreateTaskRequest, TaskItem>();

        CreateMap<User, UserDto>();
        CreateMap<CreateUserRequest, User>();
    }
}
