using LitZhu.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using User.Domain.Entities;

namespace User.Infrastructure;

public class UserDbContext(DbContextOptions<UserDbContext> options) : BaseDbContext(options)
{
    public DbSet<Users> Users { get; private set; }
    public DbSet<Roles> Roles { get; private set; }
    public DbSet<UserRoles> UserRoles { get; private set; }
    public DbSet<UserLoginHistory> UserLoginHistory { get; private set; }
}
