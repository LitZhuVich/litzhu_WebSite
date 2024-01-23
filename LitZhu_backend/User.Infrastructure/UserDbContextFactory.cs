using LitZhu.Infrastructure.EFCore;

namespace User.Infrastructure;

public class UserDbContextFactory : BaseDbContextFactory<UserDbContext>
{
    public UserDbContext? CreateDbContext<T>(string[] args)
    {
        return CreateDbContext(args);
    }
}
