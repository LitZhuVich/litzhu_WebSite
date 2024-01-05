using Article.Domain.Entities;
using Article.Infrastructure.Configs;
using LitZhu.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Reflection;

namespace Article.Infrastructure
{
    public class ArticleDbContext : BaseDbContext
    {
        public DbSet<Articles> Articles { get; private set; }

        public ArticleDbContext(DbContextOptions<ArticleDbContext> options) : base(options)
        {
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
            CultureInfo.CurrentUICulture = CultureInfo.InvariantCulture;
        }
    }
}