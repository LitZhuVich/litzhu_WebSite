using LitZhu.DomainCommons.Models;

namespace User.Domain.Entities;

public class Roles : AggregateRootEntity
{
    public string RoleName { get; private set; } = string.Empty; // 角色名
    public string RoleDesc { get; private set; } = string.Empty; // 角色描述
    public List<UserRoles> UserRoles { get; private set; } = [];

    private Roles() { }

    public static Roles Create(string roleName, string roleDesc)
    {
        return new Roles
        {
            RoleName = roleName,
            RoleDesc = roleDesc
        };
    }

    public void Update(Dictionary<string, string> update)
    {
        foreach (var item in update)
        {
            switch (item.Key)
            {
                case "roleName":
                    SetRoleName(item.Value);
                    break;
                case "roleDesc":
                    SetRoleDesc(item.Value);
                    break;
                default:
                    throw new Exception("无效的参数");
            }
        }
    }

    /// <summary>
    /// 获取当前角色的用户
    /// </summary>
    /// <returns></returns>
    public List<Users> GetUsers()
    {
        var users = UserRoles
            .Where(at => at.GetRoleId() == Id)
            .Select(at => at.GetUser())
            .ToList();
        return users;
    }

    public void SetRoleName(string roleName)
    {
        RoleName = roleName;
    }

    public void SetRoleDesc(string roleDesc)
    {
        RoleDesc = roleDesc;
    }
}
