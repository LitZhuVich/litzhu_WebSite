using Article.Domain.Entities;
using AutoMapper;
using LitZhu.WebApi.Controllers.Article.Dto;

namespace LitZhu.WebApi.Controllers.Article;

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
