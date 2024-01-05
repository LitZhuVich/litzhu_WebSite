using Microsoft.EntityFrameworkCore;

namespace LitZhu.Infrastructure.EFCore;

public abstract class BaseDbContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);

        // 通过 this.GetType().Assembly 获取当前派生类所在的程序集，并应用该程序集中的所有实体配置
        modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        // 在 OnConfiguring 方法中，配置数据库连接字符串
        optionsBuilder.UseSqlServer(new ConnectionString().SqlServerConnection);
    }
}
