using AutoMapper;
using User.Domain.DTO;
using User.Domain.Entities;

namespace LitZhu.WebApi.Controllers.User.Profiles;

public class UserRoleProfile : Profile
{
    public UserRoleProfile()
    {
        CreateMap<UserRolesDto, UserRoles>();
    }
}
