using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Globalization;

namespace LitZhu.Infrastructure.EFCore
{
    public abstract class BaseDbContextFactory<TContext> 
        : IDesignTimeDbContextFactory<TContext> where TContext : DbContext
    {
        public TContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<TContext> optionsBuilder = new DbContextOptionsBuilder<TContext>();
            optionsBuilder.UseSqlServer(new ConnectionString().SqlServerConnection);

            DbContextOptions<TContext> options = optionsBuilder.Options;

            var context = Activator.CreateInstance(typeof(TContext), options) as TContext;
            if (context == null)
            {
                throw new Exception("创建 Context 失败");
            }
            return context;
        }
    }
}