using JubilantBroccoli.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JubilantBroccoli.Infrastructure.Core.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.Email);
        builder.Property(x => x.UserName);
        builder.Property(x => x.Address).HasMaxLength(3000).IsRequired(false);
        builder.Ignore(x => x.Orders);
        builder.Property(x => x.Role).IsRequired().HasDefaultValue(AppData.UserRoleName);
    }
}