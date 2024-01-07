namespace Article.Domain.Entities;

public class ArticleTags
{
    public Guid ArticleId { get; private set; }
    public Guid TagId { get; private set; }
    public Articles Articles { get; private set; } = default!;
    public Tags Tags { get; private set; } = default!;

    private ArticleTags() { } // 添加默认的私有构造函数

    public ArticleTags(Articles articles, Tags tags)
    {
        Articles = articles;
        ArticleId = articles.Id;
        Tags = tags;
        TagId = tags.Id;
    }

    public static ArticleTags Create(Articles articles, Tags tags)
    {
        return new ArticleTags(articles, tags);
    }

    public Guid GetArticleId() => ArticleId;
    public Guid GetTagId() => TagId;

}