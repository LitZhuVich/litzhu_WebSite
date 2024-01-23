using Article.Domain.DTO;
using Article.Domain.Entities;
using AutoMapper;

namespace LitZhu.WebApi.Controllers.Article.Profiles;

public class TagProfile : Profile
{
    public TagProfile()
    {
        CreateMap<Tags, TagDto>()
            .ForMember(d => d.TagCount, opt =>
            {
                opt.MapFrom(src => src.GetArticles().Count); // 文章数
            });
        CreateMap<TagCreateDto, Tags>();
        CreateMap<TagUpdateDto, Tags>();
        CreateMap<Tags, TagUpdateDto>();
    }
}
