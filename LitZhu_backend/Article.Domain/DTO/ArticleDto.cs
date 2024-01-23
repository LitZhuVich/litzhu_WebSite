using LitZhu.DomainCommons.Models;

namespace Article.Domain.DTO;

public class ArticleDto : IBaseEntity, IHasCreationTime, IHasModificationTime
{
    public Guid Id { get; private set; }
    public string? Title { get; private set; }
    public string? Content { get; private set; }
    public Guid UserId { get; private set; } // 作者Id
    public int Likes { get; private set; } // 点赞数
    public int Views { get; private set; } // 浏览数
    public DateTime CreationTime { get; private set; }
    public DateTime? LastModificationTime { get; private set; }
}

public record ArticleCreateDto(Guid UserId, string Title, string Content);

public record ArticleUpdateDto(string? Title, string? Content, int? Likes, int? Views);