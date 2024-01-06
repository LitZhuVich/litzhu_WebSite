using LitZhu.DomainCommons.Models;

namespace Article.Domain.Entities;

public class Articles : AggregateRootEntity
{
    public string Title { get; private set; } = string.Empty; // 文章标题
    public string Content { get; private set; } = string.Empty;// 文章内容

    public List<ArticleTags> ArticleTagsList { get; private set; } = new List<ArticleTags>();

    private Articles() { }

    public Articles(string title, string content)
    {
        Title = title;
        Content = content;
    }

    public static Articles Create(string title, string content)
    {
        Articles article = new Articles(title, content);
        return article;
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
        return ArticleTagsList.Where(at => at.GetArticleId() == Id).ToList();
    }
}
