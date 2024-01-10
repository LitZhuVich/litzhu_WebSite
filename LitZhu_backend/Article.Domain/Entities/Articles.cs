using LitZhu.DomainCommons.Models;

namespace Article.Domain.Entities;

public class Articles : AggregateRootEntity
{
    public string Title { get; private set; } = string.Empty; // 文章标题
    public string Content { get; private set; } = string.Empty;// 文章内容

    public List<ArticleTags> ArticleTagsList { get; private set; } = new List<ArticleTags>();

    private Articles() { }

    public static Articles Create(string title, string content)
    {
        return new Articles
        {
            Title = title,
            Content = content
        };
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
}
