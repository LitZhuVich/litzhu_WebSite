using LitZhu.DomainCommons.Models;

namespace User.Domain.Entities;

public class UserLoginHistory : IAggregateRoot
{
    public long Id { get; init; }
    public Guid? UserId { get; init; } // 因为前端发送的登录信息用户可能不存在，所以这里允许为空
    public string? Email { get; init; }
    public string? Username { get; init; }
    public DateTime LoginTime { get; init; } = DateTime.Now; // 登录时间
    public string Message { get; init; }

    private UserLoginHistory() { }

    public static UserLoginHistory CreateEmailLoginHistory(Guid? userId, string email, string message)
    {
        return new UserLoginHistory
        {
            UserId = userId,
            Email = email,
            Message = message
        };
    }

    public static UserLoginHistory CreateUsernameLoginHistory(Guid? userId, string username, string message)
    {
        return new UserLoginHistory
        {
            UserId = userId,
            Username = username,
            Message = message
        };
    }
}
