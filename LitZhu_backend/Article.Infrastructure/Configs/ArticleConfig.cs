using Article.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Article.Infrastructure.Configs;

public class ArticleConfig : IEntityTypeConfiguration<Articles>
{
    public void Configure(EntityTypeBuilder<Articles> builder)
    {
        builder.ToTable("Articles");
        builder.HasKey(x => x.Id).IsClustered(false);// 对于Guid主键，不要建聚集索引，否则插入性能很差
        builder.HasIndex(x => x.Title); // 设置索引
        builder.HasIndex(x => x.IsDeleted); // 设置索引

        builder.Property(x => x.Title).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Content).IsRequired();

        // 设置查询过滤器，只查询未删除的数据
        builder.HasQueryFilter(b => b.IsDeleted == false);
    }
}
