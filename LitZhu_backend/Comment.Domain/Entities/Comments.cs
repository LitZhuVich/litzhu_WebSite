using LitZhu.DomainCommons.Models;

namespace Comment.Domain.Entities;

public class Comments : AggregateRootEntity
{
    public Guid UserId { get; private set; }
    public Guid ArticleId { get; private set; }
    public string Content { get; private set; } = string.Empty;

    public Comments() { }

    public static Comments Create(Guid userId, Guid articleId, string content)
    {
        Comments article = new Comments
        {
            UserId = userId,
            ArticleId = articleId,
            Content = content,
        };
        return article;
    }
}
