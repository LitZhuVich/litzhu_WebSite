using LitZhu.DomainCommons.Models;

namespace Article.Domain.Entities;

public class Tags : AggregateRootEntity
{
    public string TagName { get; private set; } = string.Empty; // 文章标签名

    public List<ArticleTags> ArticleTagsList { get; private set; } = new List<ArticleTags>();

    private Tags() { }
    public Tags(string tagName)
    {
        TagName = tagName;
    }

    public static Tags Create(string tagName)
    {
        return new Tags(tagName);
    }

    /// <summary>
    /// 获取当前标签的文章
    /// </summary>
    /// <returns></returns>
    public List<Articles> GetArticles()
    {
        var articles = ArticleTagsList
            .Where(at => at.GetTagId() == Id)
            .Select(at => at.GetArticle())
            .ToList();
        return articles;
    }
}
