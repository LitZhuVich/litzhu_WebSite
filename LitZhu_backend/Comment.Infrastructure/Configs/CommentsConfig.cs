using Comment.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Comment.Infrastructure.Configs;

public class CommentsConfig : IEntityTypeConfiguration<Comments>
{
    public void Configure(EntityTypeBuilder<Comments> builder)
    {
        builder.ToTable("Comments");
        builder.HasKey(x => x.Id).IsClustered(false);// 对于Guid主键，不要建聚集索引，否则插入性能很差
        builder.HasIndex(x => x.IsDeleted); // 设置索引


    }
}
