using LitZhu.DomainCommons.Models;

namespace Article.Domain.Entities;

public class Comments : BaseEntity, IAggregateRoot, IHasCreationTime, IHasModificationTime
{
    public Guid UserId { get; private set; }
    public Guid ArticleId { get; private set; }
    public Articles Articles { get; private set; } = default!;
    public string Content { get; private set; } = string.Empty; // 内容
    public int Likes { get; private set; } // 点赞数
    public Guid PId { get; private set; } // 父级Id
    public string Path { get; private set; } = string.Empty; // 评论路径
    public DateTime CreationTime { get; private set; } = DateTime.Now; // 创建时间
    public DateTime? LastModificationTime { get; private set; } // 最后修改时间

    public Comments() { }

    public static Comments Create(Guid userId, Guid articleId, string content)
    {
        return new Comments
        {
            UserId = userId,
            ArticleId = articleId,
            Content = content,
        };
    }

    public static Comments CreateToReply(Guid userId, Guid articleId, Guid pId, string content)
    {
        return new Comments
        {
            UserId = userId,
            ArticleId = articleId,
            PId = pId,
            Content = content,
        };
    }

    public void Update(Dictionary<string, string> update)
    {
        foreach (var item in update)
        {
            switch (item.Key)
            {
                case "content":
                    SetContent(item.Value);
                    break;
                case "likes":
                    SetLike(int.Parse(item.Value));
                    break;
                default:
                    throw new Exception("无效的参数");
            }
        }
    }

    /// <summary>
    /// 更新最后修改时间
    /// </summary>
    public void NotifyModified()
    {
        LastModificationTime = DateTime.Now;
    }

    public Articles GetArticle() => Articles;

    public void SetContent(string content)
    {
        Content = content;
    }

    public void SetLike(int like)
    {
        if (like < 0)
        {
            throw new ArgumentOutOfRangeException("点赞不能小于0");
        }
        Likes = like;
    }
}
