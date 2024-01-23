using User.Domain.Entities;

namespace User.Domain;

public interface IUserRolesRepository
{
    /// <summary>
    /// 查找用户角色。
    /// </summary>
    /// <param name="userRoles">要查找的用户角色。</param>
    /// <returns>包含找到的用户角色，如果未找到则为 null。</returns>
    Task<UserRoles?> FindUserRolesAsync(UserRoles userRoles);

    /// <summary>
    /// 创建用户角色。
    /// </summary>
    /// <param name="userRoles">要创建的用户角色。</param>
    /// <returns></returns>
    Task CreateUserRoleAsync(UserRoles userRoles);

    /// <summary>
    /// 删除用户角色。
    /// </summary>
    /// <param name="userRoles">要删除的用户角色。</param>
    /// <returns></returns>
    Task DeleteUserRolesAsync(UserRoles userRoles);

    /// <summary>
    /// 保存用户角色。
    /// </summary>
    /// <returns>并返回一个布尔值，指示保存操作是否成功。</returns>
    Task<bool> SaveUserRolesAsync();
}
