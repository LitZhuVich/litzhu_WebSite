namespace Article.Domain.Entities;

public class ArticleTags
{
    public Guid ArticleId { get; private set; }
    public Guid TagId { get; private set; }
    public Articles Articles { get; private set; } = default!;
    public Tags Tags { get; private set; } = default!;

    private ArticleTags() { } // 添加默认的私有构造函数

    public static ArticleTags Create(Guid articleId, Guid tagId)
    {
        return new ArticleTags
        {
            ArticleId = articleId,
            TagId = tagId
        };
    }

    public Guid GetArticleId() => ArticleId;
    public Guid GetTagId() => TagId;
    public Articles GetArticle() => Articles;
    public Tags GetTag() => Tags;

}