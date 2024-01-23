using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using User.Domain.Entities;

namespace User.Infrastructure.Configs;

public class UserRolesConfig : IEntityTypeConfiguration<UserRoles>
{
    public void Configure(EntityTypeBuilder<UserRoles> builder)
    {
        builder.ToTable("Users_Roles");

        // 设置 UserId 和 RoleId 作为联合主键
        builder.HasKey(x => new { x.UserId, x.RoleId });

        // 设置 UserId 和 RoleId 与 Users 和 Roles 实体之间的关联关系
        builder.HasOne(ur => ur.Users)
            .WithMany(a => a.UserRoles)
            .HasForeignKey(ur => ur.UserId);

        builder.HasOne(ur => ur.Roles)
            .WithMany(t => t.UserRoles)
            .HasForeignKey(ur => ur.RoleId);
    }
}
