using Microsoft.EntityFrameworkCore;
using User.Domain;
using User.Domain.Entities;

namespace User.Infrastructure.Repositories;

public class UserRepository(UserDbContext _db) : IUserRepository
{
    private readonly IQueryable<Users> _users
        = _db.Users.Include(x => x.UserAccessFail).Include(x => x.UserRoles).ThenInclude(ur => ur.Roles);

    public async Task AddNewLoginByEmailHistoryAsync(string email, string message)
    {
        var user = await FindUserByEmailAsync(email);
        Guid userId = Guid.Empty;
        if (user != null)
        {
            userId = user.Id;
        }
        await _db.UserLoginHistory.AddAsync(UserLoginHistory.CreateEmailLoginHistory(userId, email, message));
    }
    
    public async Task AddNewLoginByUsernameHistoryAsync(string username, string message)
    {
        var user = await FindUserByUsernameAsync(username);
        Guid? userId = null;
        if (user != null)
        {
            userId = user.Id;
        }
        await _db.UserLoginHistory.AddAsync(UserLoginHistory.CreateUsernameLoginHistory(userId, username, message));
    }

    public async Task<Users> CreateUserAsync(Users user)
    {
        var userCreateEntity = Users.Create(user.Username, user.Password);
        var userCreated = await _db.Users.AddAsync(userCreateEntity);
        return userCreated.Entity;
    }

    public async Task DeleteUserSoftAsync(Guid userId)
    {
        var user = await FindUserAsync(userId);
        if (user == null)
        {
            throw new Exception(nameof(DeleteUserSoftAsync) + "用户不存在");
        }
        user.SoftDelete();
    }

    public async Task DeleteUserTrueAsync(Guid userId)
    {
        var user = await FindUserAsync(userId);
        if (user == null)
        {
            throw new Exception(nameof(DeleteUserTrueAsync) + "用户不存在");
        }
        _db.Users.Remove(user);
    }

    public async Task<Users?> FindUserAsync(Guid userId)
    {
        return await _users.FirstOrDefaultAsync(x => x.Id == userId);
    }

    public async Task<Users?> FindUserByEmailAsync(string email)
    {
        return await _users.FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task<Users?> FindUserByUsernameAsync(string username)
    {
        return await _users.FirstOrDefaultAsync(x => x.Username == username);
    }

    public async Task<List<Users>> GetUserAsync()
    {
        return await _users.ToListAsync();
    }

    public async Task<List<Users>> GetUserDeletedAsync()
    {
        return await _users.IgnoreQueryFilters().Where(x => x.IsDeleted == true).ToListAsync();
    }

    public async Task<bool> SaveUserAsync()
    {
        return await _db.SaveChangesAsync() >= 0;
    }

    public async Task<Users> UpdateUserAsync(Users user)
    {
        if (await FindUserAsync(user.Id) == null)
        {
            throw new Exception(nameof(UpdateUserAsync) + "用户不存在");
        }
        user.NotifyModified();
        _db.Users.Update(user);
        return user;
    }
}
