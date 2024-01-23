using Article.Domain.DTO;
using Article.Domain.Entities;
using AutoMapper;

namespace LitZhu.WebApi.Controllers.Article.Profiles;

public class ArticleTagsProfile : Profile
{
    public ArticleTagsProfile()
    {
        CreateMap<ArticleTagsCreateDto, ArticleTags>();
    }
}
