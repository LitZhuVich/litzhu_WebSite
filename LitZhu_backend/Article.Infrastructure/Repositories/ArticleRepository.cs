using Article.Domain;
using Article.Domain.DTO;
using Article.Domain.Entities;
using LitZhu_Markdown;
using Microsoft.EntityFrameworkCore;

namespace Article.Infrastructure.Repositories;

public class ArticleRepository(ArticleDbContext _db) : IArticleRepository
{

    // 将查询文章的 IQueryable 对象赋值给 _article 变量。
    // 该查询使用了 Entity Framework Core 的 Include() 和 ThenInclude() 方法来包含关联的实体。
    // 具体地，它包含了 Article 实体的 ArticleTagsList 导航属性，并进一步包含了 ArticleTagsList 实体的 Tags 导航属性。
    // 这样可以在查询文章时同时加载相关的 ArticleTagsList 和 Tags 实体，以便进行后续的操作和检索。
    private IQueryable<Articles> _article
        = _db.Articles.Include(x => x.Comments).Include(x => x.ArticleTagsList).ThenInclude(at => at.Tags);

    public async Task<Articles> CreateArticleAsync(Guid userId, Articles article)
    {
        string html = new MdService().MdConvertToHtml(article.Content);
        var articleCreateEntity = Articles.Create(userId, article.Title, html);
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
        var article = await _article.FirstOrDefaultAsync(x => x.Id == articleId);
        return article;
    }

    public async Task<List<Articles>> GetArticleAsync()
    {
        var articles = await _article.ToListAsync();
        return articles;
    }

    public async Task<List<Articles>> GetArticleDeletedAsync()
    {
        var articles = await _article
            .IgnoreQueryFilters().Where(x => x.IsDeleted == true).ToListAsync();
        return articles;
    }

    public async Task<List<Articles>> GetArticleAsync(ArticleParametersDto parameters)
    {
        // 搜索条件
        if (!string.IsNullOrWhiteSpace(parameters.Q))
        {
            _article = _article.Where(a => a.Title.Contains(parameters.Q));
        }

        // 排序 TODO: 增加排序
        if (!string.IsNullOrWhiteSpace(parameters.OrderBy))
        {
            switch (parameters.OrderBy.ToLower())
            {
                case "likes":
                    _article = _article.OrderByDescending(x => x.Likes); break;
                case "views":
                    _article = _article.OrderByDescending(x => x.Views); break;
            }
        }

        var articles = await _article
            .Skip((parameters.PageIndex - 1) * parameters.PageSize).Take(parameters.PageSize)
            .ToListAsync();
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
        article.NotifyModified();
        _db.Articles.Update(article);
        return article;
    }

    public async Task<List<Articles>> GetArticleDeletedAsync(ArticleParametersDto parameters)
    {
        if (!string.IsNullOrWhiteSpace(parameters.Q))
        {
            _article = _article.Where(a => a.Title.Contains(parameters.Q));
        }

        var articles = await _article
            .IgnoreQueryFilters().Where(x => x.IsDeleted == true)
            .Skip((parameters.PageIndex - 1) * parameters.PageSize).Take(parameters.PageSize)
            .ToListAsync();
        return articles;
    }

    public async Task RecoverArticleAsync(Guid articleId)
    {
        var article = await _article
            .IgnoreQueryFilters().FirstOrDefaultAsync(x => x.Id == articleId);
        if (article == null)
        {
            throw new Exception(nameof(RecoverArticleAsync) + "文章为空");
        }
        article.Recover();
    }
}
