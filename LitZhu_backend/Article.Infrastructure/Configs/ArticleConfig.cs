using Article.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Article.Infrastructure.Configs;

public class ArticleConfig : IEntityTypeConfiguration<Articles>
{
    public void Configure(EntityTypeBuilder<Articles> builder)
    {
        builder.ToTable("Articles");
        builder.HasKey(a => a.Id).IsClustered(false);// 对于Guid主键，不要建聚集索引，否则插入性能很差
        builder.HasIndex(a => a.Title).IsUnique(); // 设置唯一索引
        builder.HasIndex(a => a.IsDeleted); // 设置索引
        builder.Property(a => a.UserId).IsRequired();

        builder.Property(a => a.Title)
                .HasMaxLength(20)
                .IsRequired();
        
        builder.Property(a => a.Content)
            .IsRequired();

        builder.Property(a => a.Desc)
            .IsRequired();

        builder.Property(a => a.Likes);

        builder.Property(a => a.Views);

        // 设置查询过滤器，只查询未删除的数据
        builder.HasQueryFilter(a => a.IsDeleted == false);
    }
}
