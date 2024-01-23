using Article.Domain.Entities;

namespace Article.Domain;

public interface IArticleTagRepository
{
    /// <summary>
    /// 根据文章ID与标签ID查询对应的数据
    /// </summary>
    /// <param name="articleTags"></param>
    /// <returns></returns>
    Task<ArticleTags?> FindArticleTagsAsync(ArticleTags articleTags);

    /// <summary>
    /// 添加文章和标签的关联
    /// </summary>
    /// <param name="articleTags"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    Task CreateArticleTagAsync(ArticleTags articleTags);

    /// <summary>
    /// 删除文章与标签之间的关联
    /// </summary>
    /// <param name="articleTags"></param>
    /// <returns></returns>
    Task DeleteArticleTagsAsync(ArticleTags articleTags);

    Task<bool> SaveArticleTagsAsync();
}
