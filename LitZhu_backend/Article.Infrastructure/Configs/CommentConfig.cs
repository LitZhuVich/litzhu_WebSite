using Article.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Article.Infrastructure.Configs;

public class CommentsConfig : IEntityTypeConfiguration<Comments>
{
    public void Configure(EntityTypeBuilder<Comments> builder)
    {
        builder.ToTable("Comments");
        
        builder.HasKey(c => c.Id).IsClustered(false);

        // 配置外键关联
        builder.HasOne(c => c.Articles)
            .WithMany(a => a.Comments)
            .HasForeignKey(c => c.ArticleId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(c => c.Content)
            .HasMaxLength(1000)
            .IsRequired();
    }
}
