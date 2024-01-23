using LitZhu.DomainCommons.Models;
using User.Domain.VO;

namespace User.Domain.Entities;

public class Users : AggregateRootEntity
{
    public string Username { get; private set; } = string.Empty;
    public string Password { get; private set; } = string.Empty;
    public Gender Gender { get; private set; } // 性别
    public string? Avatar { get; private set; } // 头像
    public string? Email { get; private set; } // 邮箱
    public string? Phone { get; private set; } // 手机号

    public UserAccessFail UserAccessFail { get; private set; }
    public List<UserRoles> UserRoles { get; private set; } = [];

    private Users()
    {
        UserAccessFail = UserAccessFail.Create(Id);
    }

    public static Users Create(string username, string password)
    {
        return new Users
        {
            Username = username,
            Password = HashHelper.HashPassword(password)
        };
    }

    public void Update(Dictionary<string, string> update)
    {
        foreach (var item in update)
        {
            switch (item.Key)
            {
                case "username":
                    SetUsername(item.Value);
                    break;
                case "password":
                    ChangePassword(item.Value);
                    break;
                case "avatar":
                    SetAvatar(item.Value);
                    break;
                case "email":
                    SetEmail(item.Value);
                    break;
                case "gender":
                    SetGender(item.Value);
                    break;
                case "phone":
                    SetPhone(item.Value);
                    break;
                default:
                    throw new Exception("无效的参数");
            }
        }
    }

    /// <summary>
    /// 修改密码
    /// </summary>
    /// <param name="password"></param>
    public string ChangePassword(string password) => Password = HashHelper.HashPassword(password);

    /// <summary>
    /// 验证密码
    /// </summary>
    /// <param name="password"></param>
    /// <returns></returns>
    public bool VerifyPassword(string password) => HashHelper.VerifyPassword(password, Password);

    /// <summary>
    /// 获取当前用户的角色
    /// </summary>
    /// <returns></returns>
    public List<Roles> GetRoles()
    {
        var roles = UserRoles
            .Where(at => at.GetUserId() == Id)
            .Select(at => at.GetRole())
            .ToList();
        return roles;
    }

    public void SetUsername(string username)
    {
        Username = username;
    }

    public void SetAvatar(string avatar)
    {
        Avatar = avatar;
    }

    public void SetGender(string gender)
    {
        if (Enum.TryParse<Gender>(gender, out var genderEnum))
        {
            Gender = genderEnum;
        }
        else
        {
            throw new Exception("无效的性别参数");
        }
    }

    public void SetEmail(string email)
    {
        Email = email;
    }

    public void SetPhone(string phone)
    {
        Phone = phone;
    }
}
