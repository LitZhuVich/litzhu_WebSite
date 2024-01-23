using Article.Domain;
using Article.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Article.Infrastructure.Repositories;

public class CommentRepository(ArticleDbContext _db) : ICommentRepository
{
    public async Task<Comments> CreateCommentAsync(Comments comment)
    {
        var commentEntity = Comments.Create(comment.UserId, comment.ArticleId, comment.Content);
        var newComment = await _db.Comments.AddAsync(commentEntity);
        return newComment.Entity;
    }

    public async Task DeleteCommentTrueAsync(Guid commentId)
    {
        var comment = await FindCommentAsync(commentId);
        if (comment == null)
        {
            throw new Exception(nameof(DeleteCommentTrueAsync) + "评论不存在");
        }
        _db.Comments.Remove(comment);
    }

    public async Task<Comments?> FindCommentAsync(Guid commentId)
    {
        var comment = await _db.Comments.Include(c => c.Articles)
            .FirstOrDefaultAsync(c => c.Id == commentId);
        return comment;
    }

    public async Task<List<Comments>> GetCommentAsync()
    {
        var comments = await _db.Comments.Include(c => c.Articles)
            .OrderByDescending(c => c.Likes).ToListAsync();
        return comments;
    }

    public async Task<Comments> ReplyToCommentAsync(Guid userId, Guid commentId, Comments comment)
    {
        var commentToReply = await FindCommentAsync(commentId);
        if (commentToReply == null)
        {
            throw new Exception(nameof(ReplyToCommentAsync) + "要回复的评论不存在");
        }

        var commentToReplyEntity = Comments.CreateToReply(userId, commentToReply.ArticleId, commentId, comment.Content);
        var newComment = await _db.Comments.AddAsync(commentToReplyEntity);
        return newComment.Entity;
    }

    public async Task<bool> SaveCommentAsync()
    {
        return await _db.SaveChangesAsync() >= 0;
    }

    public async Task<Comments> UpdateCommentAsync(Comments comment)
    {
        if (await FindCommentAsync(comment.Id) == null)
        {
            throw new Exception(nameof(UpdateCommentAsync) + "评论不存在");
        }
        comment.NotifyModified(); // 通知修改时间
        _db.Comments.Update(comment);
        return comment;
    }
}
