using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using User.Domain.Entities;

namespace User.Infrastructure.Configs;

public class UserAccessFailConfig : IEntityTypeConfiguration<UserAccessFail>
{
    public void Configure(EntityTypeBuilder<UserAccessFail> builder)
    {
        builder.ToTable("UserAccessFails");
        builder.Property("IsLockOut");

        builder.HasIndex(x => x.UserId).IsUnique();
    }
}
