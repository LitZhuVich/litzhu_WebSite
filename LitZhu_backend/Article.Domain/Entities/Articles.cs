using LitZhu.DomainCommons.Models;

namespace Article.Domain.Entities;

public class Articles : AggregateRootEntity
{
    public string Image { get; private set; } = string.Empty; // 图片
    public string Title { get; private set; } = string.Empty; // 文章标题
    public string Content { get; private set; } = string.Empty; // 文章内容
    public string Desc { get; private set; } = string.Empty; // 文章简介
    public Guid UserId { get; private set; } // 作者Id
    public int Likes { get; private set; } // 点赞数
    public int Views { get; private set; } // 浏览数
    public List<Comments> Comments { get; private set; } = []; // 评论
    public List<ArticleTags> ArticleTagsList { get; private set; } = []; // 文章标签

    private Articles() { }

    public static Articles Create(Guid userId, string image, string title, string content, string desc)
    {
        return new Articles
        {
            UserId = userId,
            Image = image,
            Title = title,
            Content = content,
            Desc = desc,
        };
    }

    public void Update(Dictionary<string, string> update)
    {
        foreach (var item in update)
        {
            switch (item.Key)
            {
                case "image":
                    SetImage(item.Value);
                    break;
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
                case "desc":
                    SetDesc(item.Value);
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

    public void SetImage(string image)
    {
        Image = image;
    }

    public void SetTitle(string title)
    {
        if (title.Length >= 20)
        {
            throw new ArgumentOutOfRangeException("标题必须小于等于20");
        }
        Title = title;
    }

    public void SetContent(string content)
    {
        Content = content;
    }

    public void SetDesc(string desc)
    {
        Desc = desc;
    }

    public void SetLike(int like)
    {
        if (like < 0)
        {
            throw new ArgumentOutOfRangeException("点赞不能小于0");
        }
        Likes = like;
    }

    public void SetView(int view)
    {
        if (view < 0)
        {
            throw new ArgumentOutOfRangeException("浏览量不能小于0");
        }
        Views = view;
    }

}
