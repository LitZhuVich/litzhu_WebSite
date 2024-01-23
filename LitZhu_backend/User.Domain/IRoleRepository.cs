using User.Domain.Entities;

namespace User.Domain;

public interface IRoleRepository
{
    /// <summary>
    /// 根据角色ID查找角色。
    /// </summary>
    /// <param name="roleId">角色ID。</param>
    /// <returns>包含找到的角色，如果未找到则为 null。</returns>
    Task<Roles?> FindRoleAsync(Guid roleId);

    /// <summary>
    /// 获取所有角色。
    /// </summary>
    /// <returns>包含角色列表。</returns>
    Task<List<Roles>> GetRoleAsync();

    /// <summary>
    /// 获取所有已删除的角色。
    /// </summary>
    /// <returns>包含已删除角色列表。</returns>
    Task<List<Roles>> GetRoleDeletedAsync();

    /// <summary>
    /// 创建角色。
    /// </summary>
    /// <param name="role">要创建的角色。</param>
    /// <returns>包含创建的角色。</returns>
    Task<Roles> CreateRoleAsync(Roles role);

    /// <summary>
    /// 更新角色。
    /// </summary>
    /// <param name="role">要更新的角色。</param>
    /// <returns>包含更新后的角色。</returns>
    Task<Roles> UpdateRoleAsync(Roles role);

    /// <summary>
    /// 软删除角色。
    /// </summary>
    /// <param name="roleId">要删除的角色ID。</param>
    /// <returns></returns>
    Task DeleteRoleSoftAsync(Guid roleId);

    /// <summary>
    /// 永久删除角色。
    /// </summary>
    /// <param name="roleId">要删除的角色ID。</param>
    /// <returns></returns>
    Task DeleteRoleTrueAsync(Guid roleId);

    /// <summary>
    /// 保存对角色的更改。
    /// </summary>
    /// <returns>布尔值，指示更改是否成功保存。</returns>
    Task<bool> SaveRoleAsync();
}