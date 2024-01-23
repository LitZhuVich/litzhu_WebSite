using LitZhu.DomainCommons.Models;
using User.Domain.VO;
namespace User.Domain.DTO;

public class UserDto : IBaseEntity, IHasCreationTime, IHasModificationTime
{
    public Guid Id { get; private set; }
    public string Username { get; private set; } = string.Empty; // 用户名
    public string? gender { get; private set; } // 性别
    public string? Avatar { get; private set; } // 头像
    public string? Email { get; private set; } // 邮箱
    public string? Phone { get; private set; } // 手机号
    public DateTime CreationTime { get; private set; }
    public DateTime? LastModificationTime { get; private set; }
}

public record UserCreateByUsernameDto(string Username, string Password);

public record UserCreateByEmailDto(string Email, string Password);

public record UserUpdateDto(
    string? Username,
    string? Password,
    Gender? Gender,
    string? Email,
    string? Avatar,
    string? Phone
);
