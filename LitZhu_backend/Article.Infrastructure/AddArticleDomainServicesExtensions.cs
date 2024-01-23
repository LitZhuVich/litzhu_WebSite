using Article.Domain;
using Article.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Article.Infrastructure;

public static class AddArticleDomainServicesExtensions
{
    public static IServiceCollection AddArticleDomainServices(this IServiceCollection services)
    {
        services.AddDbContext<ArticleDbContext>();

        services.AddScoped<IArticleRepository, ArticleRepository>();
        services.AddScoped<ITagRepository, TagRepository>();
        services.AddScoped<IArticleTagRepository, ArticleTagRepository>();
        services.AddScoped<ICommentRepository, CommentRepository>();
        services.AddScoped<ArticleDomainService>();

        return services;
    }
}
