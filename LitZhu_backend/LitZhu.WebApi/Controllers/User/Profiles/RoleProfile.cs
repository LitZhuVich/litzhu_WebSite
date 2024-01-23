using AutoMapper;
using User.Domain.DTO;
using User.Domain.Entities;

namespace LitZhu.WebApi.Controllers.User.Profiles;

public class RoleProfile : Profile
{
    public RoleProfile()
    {
        CreateMap<Roles, RoleDto>();
        CreateMap<RoleCreateDto, Roles>();
        CreateMap<RoleUpdateDto, Roles>();
        CreateMap<Roles, RoleUpdateDto>();
    }
}
