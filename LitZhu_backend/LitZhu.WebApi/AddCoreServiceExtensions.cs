namespace LitZhu.WebApi;

public static class AddCoreServiceExtensions
{
    /// <summary>
    /// Cors策略名称
    /// </summary>
    private const string CorsPolicyName = "AllowSpecificOrigin";

    /// <summary>
    /// 域名
    /// </summary>
    private const string DomainName = "http://localhost:5173";

    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        services.AddCors(opt =>
        {
            opt.AddPolicy(CorsPolicyName,
                builder =>
                {
                    builder.WithOrigins(DomainName)
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
        });

        return services;
    }

    public static IApplicationBuilder UseCoreServices(this IApplicationBuilder app)
    {
        app.UseCors(CorsPolicyName);
        return app;
    }
}
