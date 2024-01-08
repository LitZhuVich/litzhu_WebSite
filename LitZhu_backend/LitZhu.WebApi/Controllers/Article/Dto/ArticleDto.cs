using Article.Domain.Entities;
using LitZhu.DomainCommons.Models;

namespace LitZhu.WebApi.Controllers.Article.Dto;

public class ArticleDto : IBaseEntity, IHasCreationTime, IHasModificationTime
{
    public Guid Id { get; private set; }
    public string? Title { get; private set; }
    public string? Content { get; private set; }
    public DateTime CreationTime { get; private set; }
    public DateTime? LastModificationTime { get; private set; }
}
