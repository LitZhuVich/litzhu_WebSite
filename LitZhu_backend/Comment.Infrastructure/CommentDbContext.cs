using Comment.Domain.Entities;
using LitZhu.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Comment.Infrastructure
{
    public class CommentDbContext : BaseDbContext
    {
        public DbSet<Comments> Comments { get; private set; }

        public CommentDbContext(DbContextOptions<CommentDbContext> options) : base(options)
        {
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
            CultureInfo.CurrentUICulture = CultureInfo.InvariantCulture;
        }
    }
}