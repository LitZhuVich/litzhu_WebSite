using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using User.Domain;

namespace User.Infrastructure;

public class EmailCodeSender
    (ILogger<EmailCodeSender> _logger, ConnectionMultiplexer _cache) : IEmailCodeSender
{
    public async Task<string?> FindEmailCodeAsync(string email)
    {
        // 发送邮箱的缓存Key
        string key = $"EmailCode_{email}";
        string? code = await _cache.GetDatabase().StringGetAsync(key); // 从缓存中获取验证码
        await _cache.GetDatabase().KeyDeleteAsync(key); // 从缓存中删除验证码
        return code; // 返回验证码
    }

    public Task SaveEmailCodeAsync(string email, string code)
    {
        string key = $"EmailCode_{email}";
        TimeSpan timeSpan = TimeSpan.FromMinutes(5); // 5分钟过期
        return _cache.GetDatabase().StringSetAsync(key, code, timeSpan); // key,value,过期时间
    }

    public async Task<string> SendEmailCodeAsync(string email)
    {
        string code = new Random().Next(10000, 99999).ToString();
        await SaveEmailCodeAsync(email, code);
        _logger.LogWarning($"向邮箱地址：{email} , 发送验证码：{code}");
        return code;
    }
}
