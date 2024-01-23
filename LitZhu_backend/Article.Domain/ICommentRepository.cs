using Article.Domain.Entities;

namespace Article.Domain;

public interface ICommentRepository
{
    /// <summary>
    /// 获取评论的异步方法。
    /// </summary>
    /// <returns>未删除的评论列表</returns>
    Task<List<Comments>> GetCommentAsync();

    /// <summary>
    /// 根据评论 ID 查找评论的异步方法。
    /// </summary>
    /// <param name="commentId">评论 ID</param>
    /// <returns>匹配的评论对象，如果找不到则返回 null</returns>
    Task<Comments?> FindCommentAsync(Guid commentId);

    /// <summary>
    /// 创建评论的异步方法。
    /// </summary>
    /// <param name="comment">要创建的评论对象</param>
    /// <returns>创建的评论对象</returns>
    Task<Comments> CreateCommentAsync(Comments comment);

    /// <summary>
    /// 彻底删除评论的异步方法。
    /// </summary>
    /// <param name="commentId">要删除的评论 ID</param>
    Task DeleteCommentTrueAsync(Guid commentId);

    /// <summary>
    /// 更新评论的异步方法。
    /// </summary>
    /// <param name="comment">要更新的评论对象</param>
    /// <returns>更新后的评论对象</returns>
    Task<Comments> UpdateCommentAsync(Comments comment);

    /// <summary>
    /// 回复评论的异步方法。
    /// </summary>
    /// <param name="userId">用户Id</param>
    /// <param name="commentId">评论Id</param>
    /// <param name="comment">要创建的评论对象</param>
    /// <returns></returns>
    Task<Comments> ReplyToCommentAsync(Guid userId, Guid commentId, Comments comment);

    /// <summary>
    /// 保存更改到数据库的异步方法。
    /// </summary>
    /// <returns>操作是否成功的布尔值</returns>
    Task<bool> SaveCommentAsync();
}