using LitZhu.DomainCommons.Models;

namespace Article.Domain.DTO;

public class CommentDto : IBaseEntity, IHasCreationTime, IHasModificationTime
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public string Content { get; private set; } = string.Empty; // 内容
    public int Likes { get; private set; } // 点赞数
    public DateTime CreationTime { get; private set; } = DateTime.Now; // 创建时间
    public DateTime? LastModificationTime { get; private set; } // 最后修改时间
}

public record CommentCreateDto(Guid UserId, Guid ArticleId, string Content);

public record CommentUpdateDto(Guid? UserId, Guid? ArticleId, string? Content);