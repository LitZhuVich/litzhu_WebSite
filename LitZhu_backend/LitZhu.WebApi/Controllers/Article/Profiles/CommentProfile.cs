using Article.Domain.DTO;
using Article.Domain.Entities;
using AutoMapper;

namespace LitZhu.WebApi.Controllers.Article.Profiles;

public class CommentProfile : Profile
{
    public CommentProfile()
    {
        CreateMap<Comments, CommentDto>();
        CreateMap<CommentCreateDto, Comments>();
        CreateMap<CommentUpdateDto, Comments>();
        CreateMap<Comments, CommentUpdateDto>();
    }
}
