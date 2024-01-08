using Article.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Article.Infrastructure.Configs;

public class ArticleTagsConfig : IEntityTypeConfiguration<ArticleTags>
{
    public void Configure(EntityTypeBuilder<ArticleTags> builder)
    {
        builder.ToTable("Articles_Tags");

        // 设置 ArticleId 和 TagId 作为联合主键
        builder.HasKey(at => new { at.ArticleId, at.TagId });

        // 设置 ArticleId 和 TagId 与 Articles 和 Tags 实体之间的关联关系
        builder.HasOne(at => at.Articles)
            .WithMany(a => a.ArticleTagsList)
            .HasForeignKey(at => at.ArticleId);

        builder.HasOne(at => at.Tags)
            .WithMany(t => t.ArticleTagsList)
            .HasForeignKey(at => at.TagId);
    }
}
