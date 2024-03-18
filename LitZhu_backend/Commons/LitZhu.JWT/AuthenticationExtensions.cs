using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LitZhu.JWT;

public static class AuthenticationExtensions
{
    /// <summary>
    /// 向指定的<see cref="IServiceCollection"/>添加JWT身份验证
    /// </summary>
    /// <param name="services">要添加身份验证服务的<see cref="IServiceCollection"/></param>
    /// <param name="jwtOpt">JWT配置选项</param>
    /// <returns><see cref="AuthenticationBuilder"/>实例</returns>
    public static AuthenticationBuilder AddJWTAuthentication(this IServiceCollection services, JWTOptions jwtOpt)
    {
        return services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new()
                {
                    // 验证颁发者
                    ValidateIssuer = true,
                    ValidIssuer = jwtOpt.Issuer,

                    // 验证接收者
                    ValidateAudience = true,
                    ValidAudience = jwtOpt.Audience,

                    // 验证令牌有效期
                    ValidateLifetime = true,

                    // 验证签名密钥
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOpt.Key)),

                    // 允许的时间偏移量
                    ClockSkew = TimeSpan.FromSeconds(jwtOpt.ExpireSeconds)
                };
            });
    }

    public static IServiceCollection AddJwtServices(this IServiceCollection services)
    {
        services.AddScoped<IJwtService, JwtService>();
        services.AddSingleton<IAuthorizationMiddlewareResultHandler, CustomAuthorizationMiddlewareResultHandler>();

        return services;
    }
}