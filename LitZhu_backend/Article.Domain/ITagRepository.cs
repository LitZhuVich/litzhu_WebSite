using Article.Domain.Entities;

namespace Article.Domain;

public interface ITagRepository
{
    /// <summary>
    /// 获取未删除的标签的异步方法。
    /// </summary>
    /// <returns>未删除的标签列表</returns>
    Task<List<Tags>> GetTagAsync();

    /// <summary>
    /// 获取已删除的标签的异步方法。
    /// </summary>
    /// <returns>已删除的标签列表</returns>
    Task<List<Tags>> GetTagDeletedAsync();

    /// <summary>
    /// 根据标签 ID 查找标签的异步方法。
    /// </summary>
    /// <param name="tagId">标签 ID</param>
    /// <returns>匹配的标签对象，如果找不到则返回 null</returns>
    Task<Tags?> FindTagAsync(Guid tagId);

    /// <summary>
    /// 根据标签名称查找标签的异步方法。
    /// </summary>
    /// <param name="tagName">标签名称</param>
    /// <returns>匹配的标签对象，如果找不到则返回 null</returns>
    Task<Tags?> FindTagAsync(string tagName);

    /// <summary>
    /// 创建标签的异步方法。
    /// </summary>
    /// <param name="tag">要创建的标签对象</param>
    /// <returns>创建的标签对象</returns>
    Task<Tags> CreateTagAsync(Tags tag);

    /// <summary>
    /// 软删除标签的异步方法。
    /// </summary>
    /// <param name="tagId">要删除的标签 ID</param>
    Task DeleteTagSoftAsync(Guid tagId);

    /// <summary>
    /// 彻底删除标签的异步方法。
    /// </summary>
    /// <param name="tagId">要删除的标签 ID</param>
    Task DeleteTagTrueAsync(Guid tagId);

    /// <summary>
    /// 更新标签的异步方法。
    /// </summary>
    /// <param name="tag">要更新的标签对象</param>
    /// <returns>更新后的标签对象</returns>
    Task<Tags> UpdateTagAsync(Tags tag);

    /// <summary>
    /// 保存更改到数据库的异步方法。
    /// </summary>
    /// <returns>操作是否成功的布尔值</returns>
    Task<bool> SaveTagAsync();
}