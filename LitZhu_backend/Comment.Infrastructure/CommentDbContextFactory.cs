using LitZhu.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Globalization;

namespace Comment.Infrastructure;

public class CommentDbContextFactory : BaseDbContextFactory<CommentDbContext>
{
    public CommentDbContext? CreateDbContext<T>(string[] args)
    {
        return CreateDbContext(args);
    }
}