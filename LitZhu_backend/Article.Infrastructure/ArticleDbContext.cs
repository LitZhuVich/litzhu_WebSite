using Article.Domain.Entities;
using LitZhu.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Article.Infrastructure;

public class ArticleDbContext(DbContextOptions<ArticleDbContext> options) : BaseDbContext(options)
{
    public DbSet<Articles> Articles { get; private set; }
    public DbSet<Tags> Tags { get; private set; }
    public DbSet<ArticleTags> ArticleTags { get; private set; }
}
