using Article.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using User.Domain;
using User.Domain.Entities;

namespace User.Infrastructure.Repositories;

public class UserRolesRepository(UserDbContext _db) : IUserRolesRepository
{
    public async Task CreateUserRoleAsync(UserRoles userRoles)
    {
        var userRole = UserRoles.Create(userRoles.UserId, userRoles.RoleId);
        await _db.UserRoles.AddAsync(userRole);
    }

    public async Task DeleteUserRolesAsync(UserRoles userRoles)
    {
        var userRole = await FindUserRolesAsync(userRoles);
        if (userRole == null)
        {
            throw new Exception(nameof(DeleteUserRolesAsync) + "用户和角色之间的关联不存在");
        }

        _db.UserRoles.Remove(userRole);
    }

    public async Task<UserRoles?> FindUserRolesAsync(UserRoles userRoles)
    {
        return await _db.UserRoles.FirstOrDefaultAsync(x => x.UserId == userRoles.UserId && x.RoleId == userRoles.RoleId);
    }

    public async Task<bool> SaveUserRolesAsync()
    {
        return await _db.SaveChangesAsync() >= 0;
    }
}
