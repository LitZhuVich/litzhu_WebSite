using AutoMapper;
using User.Domain.DTO;
using User.Domain.Entities;

namespace LitZhu.WebApi.Controllers.User.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<Users, UserDto>()
            .ForMember(u => u.gender, opt =>
            {
                opt.MapFrom(src => src.Gender.ToString());
            });
        CreateMap<UserCreateByUsernameDto, Users>();
        CreateMap<UserCreateByEmailDto, Users>();
        CreateMap<UserUpdateDto, Users>();
        CreateMap<Users, UserUpdateDto>();
    }
}
