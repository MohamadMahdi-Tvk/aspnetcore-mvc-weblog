using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeblogSample.Data.Entities;

namespace WeblogSample.Data.Configs;

public class RoleConfig : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.Property(p => p.Name).HasMaxLength(20).IsRequired();

        builder.HasData(
            new Role { Id = RolesConsts.AdminRoleId, Name = RolesConsts.AdminRoleName },
            new Role { Id = RolesConsts.UserRoleId, Name = RolesConsts.UserRoleName }
        );
    }
}
public static class RolesConsts
{
    public const short AdminRoleId = 1;
    public const short UserRoleId = 2;

    public const string AdminRoleName = "Admin";
    public const string UserRoleName = "User";
}