using Microsoft.EntityFrameworkCore;
using User.Domain;
using User.Domain.Entities;

namespace User.Infrastructure.Repositories;

public class RoleRepository(UserDbContext _db) : IRoleRepository
{
    private readonly IQueryable<Roles> _roles
        = _db.Roles.Include(x => x.UserRoles).ThenInclude(ur => ur.Users);

    public async Task<Roles> CreateRoleAsync(Roles role)
    {
        var roleCreateEntity = Roles.Create(role.RoleName, role.RoleDesc);
        var roleDto = await _db.Roles.AddAsync(roleCreateEntity);
        return roleDto.Entity;
    }

    public async Task DeleteRoleSoftAsync(Guid roleId)
    {
        var role = await FindRoleAsync(roleId);
        if (role == null)
        {
            throw new Exception(nameof(DeleteRoleSoftAsync) + "角色不存在");
        }
        role.SoftDelete();
    }

    public async Task DeleteRoleTrueAsync(Guid roleId)
    {
        var role = await FindRoleAsync(roleId);
        if (role == null)
        {
            throw new Exception(nameof(DeleteRoleSoftAsync) + "角色不存在");
        }
        _db.Roles.Remove(role);
    }

    public async Task<Roles?> FindRoleAsync(Guid roleId)
    {
        return await _roles.FirstOrDefaultAsync(x => x.Id == roleId);
    }

    public async Task<List<Roles>> GetRoleAsync()
    {
        return await _roles.ToListAsync();
    }

    public async Task<List<Roles>> GetRoleDeletedAsync()
    {
        return await _roles.IgnoreQueryFilters().Where(x => x.IsDeleted == true).ToListAsync();
    }

    public async Task<bool> SaveRoleAsync()
    {
        return await _db.SaveChangesAsync() >= 0;
    }

    public async Task<Roles> UpdateRoleAsync(Roles role)
    {
        if (await FindRoleAsync(role.Id) == null)
        {
            throw new Exception(nameof(UpdateRoleAsync) + "角色不存在");
        }
        role.NotifyModified();
        _db.Roles.Update(role);
        return role;
    }
}
