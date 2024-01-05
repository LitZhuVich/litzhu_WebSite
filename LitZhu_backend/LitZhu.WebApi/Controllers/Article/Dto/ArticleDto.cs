using LitZhu.DomainCommons.Models;

namespace Article.WebApi.Dto;

public class ArticleDto : IBaseEntity, IHasCreationTime, IHasModificationTime
{
    public Guid Id { get; private set; }
    public string? Title { get; private set; }
    public string? Content { get; private set; }
    public DateTime CreationTime { get; private set; } // 创建时间
    public DateTime? LastModificationTime { get; private set; } // 最后修改时间
}
