using LitZhu.DomainCommons.Models;

namespace Article.Domain.Entities;

public class Articles : AggregateRootEntity
{
    public string Title { get; private set; } = string.Empty; // 文章标题
    public string Content { get; private set; } = string.Empty; // 文章内容
    public Guid UserId { get; private set; } // 作者Id
    public int Likes { get; private set; } // 点赞数
    public int Views { get; private set; } // 浏览数
    public List<Comments> Comments { get; private set; } = []; // 评论
    public List<ArticleTags> ArticleTagsList { get; private set; } = []; // 文章标签

    private Articles() { }

    public static Articles Create(Guid userId, string title, string content)
    {
        return new Articles
        {
            UserId = userId,
            Title = title,
            Content = content
        };
    }

    public void Update(Dictionary<string, string> update)
    {
        foreach (var item in update)
        {
            switch (item.Key)
            {
                case "title":
                    SetTitle(item.Value);
                    break;
                case "content":
                    SetContent(item.Value);
                    break;
                case "likes":
                    SetLike(int.Parse(item.Value));
                    break;
                case "views":
                    SetView(int.Parse(item.Value));
                    break;
                default:
                    throw new Exception("无效的参数");
            }
        }
    }

    /// <summary>
    /// 获取当前文章的标签
    /// </summary>
    /// <returns></returns>
    public List<Tags> GetTags()
    {
        var tags = ArticleTagsList
            .Where(at => at.GetArticleId() == Id)
            .Select(at => at.GetTag())
            .ToList();
        return tags;
    }

    /// <summary>
    /// 获取当前文章的评论
    /// </summary>
    /// <returns></returns>
    public List<Comments> GetComments() => Comments.Where(c => c.ArticleId == Id).ToList();

    public void SetTitle(string title)
    {
        if (title.Length >= 50)
        {
            throw new ArgumentOutOfRangeException("标题必须小于等于50");
        }
        Title = title;
    }
    public void SetContent(string content)
    {
        Content = content;
    }
    public void SetLike(int like)
    {
        Likes = like;
    }
    public void SetView(int view)
    {
        Views = view;
    }

}
