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
        Tags tag = new Tags(tagName);
        return tag;
    }

    public void AddArticleTags(ArticleTags articleTags)
    {
        ArticleTagsList.Add(articleTags);
    }

    public void RemoveArticleTags(ArticleTags articleTags)
    {
        ArticleTagsList.Remove(articleTags);
    }

    public List<ArticleTags> GetArticleTags()
    {
        return ArticleTagsList.Where(at => at.GetTagId() == Id).ToList();
    }
}
