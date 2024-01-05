using LitZhu.DomainCommons.Models;

namespace Article.Domain.Entities;

public class Articles : AggregateRootEntity
{
    public string Title { get; private set; } // 标题
    public string Content { get; private set; } // 内容

    public Articles() { }

    public static Articles Create(string title, string content)
    {
        Articles article = new Articles
        {
            Title = title,
            Content = content,
        };
        return article;
    }
}
