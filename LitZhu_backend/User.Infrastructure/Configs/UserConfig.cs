using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using User.Domain.Entities;

namespace User.Infrastructure.Configs;

public class UserConfig : IEntityTypeConfiguration<Users>
{
    public void Configure(EntityTypeBuilder<Users> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(x => x.Id).IsClustered(false);// 对于Guid主键，不要建聚集索引，否则插入性能很差

        builder.HasIndex(x => x.Username).IsUnique();
        builder.HasIndex(x => x.IsDeleted);
        // 设置查询过滤器，只查询未删除的数据
        builder.HasQueryFilter(a => a.IsDeleted == false);

        builder.Property(x => x.Username).IsRequired().HasMaxLength(10);
        builder.Property(x => x.Password).IsRequired().HasMaxLength(100).IsUnicode(false);
    }
}
