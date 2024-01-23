using Article.Domain.DTO;
using Article.Domain.Entities;
using AutoMapper;

namespace LitZhu.WebApi.Controllers.Article.Profiles;

public class ArticleProfile : Profile
{
    public ArticleProfile()
    {
        CreateMap<Articles, ArticleDto>();
        CreateMap<ArticleCreateDto, Articles>();
        CreateMap<ArticleUpdateDto, Articles>();
        CreateMap<Articles, ArticleUpdateDto>();
    }
}
