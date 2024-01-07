using Article.Domain.Entities;

namespace Article.Domain;
public interface IArticleRepository
{
    /// <summary>
    /// 获取未被删除的文章的异步方法
    /// </summary>
    /// <returns>未删除的文章列表</returns>
    Task<List<Articles>> GetArticleAsync();

    /// <summary>
    /// 获取已被删除的文章的异步方法
    /// </summary>
    /// <returns>已删除的文章列表</returns>
    Task<List<Articles>> GetArticleDeletedAsync();

    /// <summary>
    /// 根据文章ID异步查找文章
    /// </summary>
    /// <param name="articleId">文章ID</param>
    /// <returns>找到的文章，如果不存在则为 null</returns>
    Task<Articles?> FindArticleAsync(Guid articleId);

    /// <summary>
    /// 异步创建新文章
    /// </summary>
    /// <param name="createDto">创建文章的DTO</param>
    /// <returns>创建的文章</returns>
    Task<Articles> CreateArticleAsync(Articles article);

    /// <summary>
    /// 异步更新文章
    /// </summary>
    /// <param name="article">要更新的文章</param>
    /// <returns>更新后的文章</returns>
    Task<Articles> UpdateArticleAsync(Articles article);

    /// <summary>
    /// 异步删除文章
    /// </summary>
    /// <param name="article">要删除的文章</param>
    Task DeleteTrueArticleAsync(Guid articleId);

    /// <summary>
    /// 异步软删除文章
    /// </summary>
    /// <param name="article">要删除的文章</param>
    Task DeleteSoftArticleAsync(Guid articleId);

    /// <summary>
    /// 异步保存对文章的更改
    /// </summary>
    /// <returns>保存操作是否成功的布尔值</returns>
    Task<bool> SaveArticleAsync();
}