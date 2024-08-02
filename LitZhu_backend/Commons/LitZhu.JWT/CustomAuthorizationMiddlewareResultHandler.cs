using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

// 自定义授权结果处理器
public class CustomAuthorizationMiddlewareResultHandler : IAuthorizationMiddlewareResultHandler
{
    public async Task HandleAsync(RequestDelegate next, HttpContext context, AuthorizationPolicy policy, PolicyAuthorizationResult authorizeResult)
    {
        if (!authorizeResult.Succeeded)
        {
            // 构建 JSON 响应的内容
            var response = new
            {
                code = 401,
                error = "Unauthorized",
                message = "授权失败"
            };
            
            // 设置响应的内容类型为 JSON
            context.Response.ContentType = "application/json";
            // 设置响应的状态码为 401 未授权
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;

            // 将 JSON 响应写入响应流
            var json = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(json);
            return;
        }

        // 验证成功，继续处理下一个中间件
        await next(context);
    }
}
