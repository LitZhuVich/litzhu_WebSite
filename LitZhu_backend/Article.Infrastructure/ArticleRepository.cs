using Article.Domain;
using Article.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Article.Infrastructure;

public class ArticleRepository(ArticleDbContext _db) : IArticleRepository
{
    public async Task<Articles> CreateArticleAsync(Articles article)
    {
        var articleCreateEntity = Articles.Create(article.Title, article.Content);
        var newArticle = await _db.Articles.AddAsync(articleCreateEntity);
        return newArticle.Entity;
    }

    public async Task DeleteTrueArticleAsync(Guid articleId)
    {
        var article = await FindArticleAsync(articleId);
        if (article == null)
        {
            throw new Exception(nameof(DeleteTrueArticleAsync) + "文章ID为空");
        }
        _db.Articles.Remove(article);
    }

    public async Task DeleteSoftArticleAsync(Guid articleId)
    {
        var article = await FindArticleAsync(articleId);
        if (article == null)
        {
            throw new Exception(nameof(DeleteSoftArticleAsync) + "文章ID为空");
        }
        article.SoftDelete();
    }

    public async Task<Articles?> FindArticleAsync(Guid articleId)
    {
        var article = await _db.Articles.FirstOrDefaultAsync(x => x.Id == articleId);
        return article;
    }
    
    public async Task<List<Articles>> GetArticleAsync()
    {
        var articles = await _db.Articles.ToListAsync();
        return articles;
    }

    public async Task<bool> SaveArticleAsync()
    {
       return await _db.SaveChangesAsync() >= 0;
    }

    public async Task<Articles> UpdateArticleAsync(Articles article)
    {
        if (await FindArticleAsync(article.Id) == null)
        {
            throw new Exception(nameof(UpdateArticleAsync) + "文章ID为空");
        }
        _db.Articles.Update(article);
        return article;
    }

    public async Task<List<Articles>> GetArticleDeletedAsync()
    {
        var articles = await _db.Articles.IgnoreQueryFilters()
            .Where(x=>x.IsDeleted == true).ToListAsync();
        return articles;
    }
}
