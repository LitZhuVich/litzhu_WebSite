using Article.Domain;
using Article.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Article.Infrastructure.Repositories;

public class ArticleTagRepository(ArticleDbContext _db) : IArticleTagRepository
{

    public async Task<ArticleTags?> FindArticleTagsAsync(ArticleTags articleTags)
    {
        var articleTag = await _db.ArticleTags
            .FirstOrDefaultAsync(at => at.ArticleId == articleTags.ArticleId && at.TagId == articleTags.TagId);
        return articleTag;
    }

    public async Task CreateArticleTagAsync(ArticleTags articleTags)
    {
        var articleTag = ArticleTags.Create(articleTags.ArticleId, articleTags.TagId);
        await _db.ArticleTags.AddAsync(articleTag);
    }

    public async Task DeleteArticleTagsAsync(ArticleTags articleTags)
    {
        var articleTag = await FindArticleTagsAsync(articleTags);
        if (articleTag == null)
        {
            throw new Exception(nameof(DeleteArticleTagsAsync) + "文章与标签之间的关联不存在");
        }

        _db.ArticleTags.Remove(articleTag);
    }

    public async Task<bool> SaveArticleTagsAsync()
    {
        return await _db.SaveChangesAsync() >= 0;
    }
}
