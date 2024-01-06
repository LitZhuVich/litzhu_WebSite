using Article.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Article.Infrastructure.Configs;

public class TagConfig : IEntityTypeConfiguration<Tags>
{
    public void Configure(EntityTypeBuilder<Tags> builder)
    {
        builder.ToTable("Tags");

        builder.Property(t => t.TagName).IsRequired();

        builder.HasIndex(at => at.IsDeleted); // 设置索引

        // 设置查询过滤器，只查询未删除的数据
        builder.HasQueryFilter(at => at.IsDeleted == false);
    }
}
