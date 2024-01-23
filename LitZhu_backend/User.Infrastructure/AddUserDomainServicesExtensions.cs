using Microsoft.Extensions.DependencyInjection;
using User.Domain;
using User.Infrastructure.Repositories;

namespace User.Infrastructure;

public static class AddUserDomainServicesExtensions
{
    public static IServiceCollection AddUserDomainServices(this IServiceCollection services)
    {
        services.AddDbContext<UserDbContext>();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IEmailCodeSender, EmailCodeSender>();
        services.AddScoped<IUserRolesRepository, UserRolesRepository>();
        services.AddScoped<UserDomainService>();

        return services;
    }
}
