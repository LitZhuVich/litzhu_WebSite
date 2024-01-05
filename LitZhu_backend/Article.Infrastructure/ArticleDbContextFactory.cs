using Article.Infrastructure;
using LitZhu.Infrastructure.EFCore;

namespace Comment.Infrastructure;

public class ArticleDbContextFactory : BaseDbContextFactory<ArticleDbContext>
{
    public ArticleDbContext? CreateDbContext<T>(string[] args)
    {
        return CreateDbContext(args);
    }
}
