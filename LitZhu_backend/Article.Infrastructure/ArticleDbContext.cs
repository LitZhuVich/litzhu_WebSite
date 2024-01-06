using Article.Domain.Entities;
using LitZhu.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Article.Infrastructure;

public class ArticleDbContext : BaseDbContext
{
    public DbSet<Articles> Articles { get; private set; }
    public DbSet<Tags> Tags { get; private set; }
    public DbSet<ArticleTags> ArticleTags { get; private set; }

    public ArticleDbContext(DbContextOptions<ArticleDbContext> options) : base(options)
    {
        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
        CultureInfo.CurrentUICulture = CultureInfo.InvariantCulture;
    }
}
