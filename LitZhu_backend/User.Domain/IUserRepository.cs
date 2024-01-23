using User.Domain.Entities;

namespace User.Domain;

public interface IUserRepository
{
    /// <summary>
    /// 通过邮件查找用户
    /// </summary>
    /// <param name="email">邮件地址</param>
    /// <returns>找到的用户对象，如果未找到则为 null</returns>
    Task<Users?> FindUserByEmailAsync(string email);

    /// <summary>
    /// 通过用户名查找用户
    /// </summary>
    /// <param name="username">用户名</param>
    /// <returns>找到的用户对象，如果未找到则为 null</returns>
    Task<Users?> FindUserByUsernameAsync(string username);

    /// <summary>
    /// 通过用户ID查找用户
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns>找到的用户对象，如果未找到则为 null</returns>
    Task<Users?> FindUserAsync(Guid userId);

    /// <summary>
    /// 获取所有用户
    /// </summary>
    /// <returns>用户列表</returns>
    Task<List<Users>> GetUserAsync();

    /// <summary>
    /// 获取所有已被删除的用户
    /// </summary>
    /// <returns>用户列表</returns>
    Task<List<Users>> GetUserDeletedAsync();

    /// <summary>
    /// 创建用户
    /// </summary>
    /// <param name="user">要创建的用户对象</param>
    /// <returns>创建后的用户对象</returns>
    Task<Users> CreateUserAsync(Users user);

    /// <summary>
    /// 更新用户信息
    /// </summary>
    /// <param name="user">要更新的用户对象</param>
    /// <returns>更新后的用户对象</returns>
    Task<Users> UpdateUserAsync(Users user);

    /// <summary>
    /// 软删除用户
    /// </summary>
    /// <param name="userId">要删除的用户ID</param>
    Task DeleteUserSoftAsync(Guid userId);

    /// <summary>
    /// 真软删除用户
    /// </summary>
    /// <param name="userId">要删除的用户ID</param>
    Task DeleteUserTrueAsync(Guid userId);

    /// <summary>
    /// 保存用户数据
    /// </summary>
    /// <returns>保存操作是否成功</returns>
    Task<bool> SaveUserAsync();

    /// <summary>
    /// 添加新的登录历史记录
    /// </summary>
    /// <param name="email">电子邮件地址</param>
    /// <param name="message">登录历史记录信息</param>
    Task AddNewLoginByEmailHistoryAsync(string email, string message);

    /// <summary>
    /// 添加新的登录历史记录
    /// </summary>
    /// <param name="username">用户名</param>
    /// <param name="message">登录历史记录信息</param>
    Task AddNewLoginByUsernameHistoryAsync(string username, string message);
}
