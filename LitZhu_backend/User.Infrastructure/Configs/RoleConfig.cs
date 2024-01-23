using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using User.Domain.Entities;

namespace User.Infrastructure.Configs;

public class RoleConfig : IEntityTypeConfiguration<Roles>
{
    public void Configure(EntityTypeBuilder<Roles> builder)
    {
        builder.ToTable("Roles");
        builder.HasKey(x => x.Id).IsClustered(false);// 对于Guid主键，不要建聚集索引，否则插入性能很差

        builder.Property(t => t.RoleName).IsRequired().HasMaxLength(10);

        builder.HasIndex(x => x.IsDeleted);
        // 设置查询过滤器，只查询未删除的数据
        builder.HasQueryFilter(a => a.IsDeleted == false);
    }
}
