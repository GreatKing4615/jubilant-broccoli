using JubilantBroccoli.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JubilantBroccoli.Infrastructure.Core.Configurations;

public class ItemConfiguration : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder.ToTable("Items");
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.CookingTime).IsRequired();
        builder.Property(x => x.Type).IsRequired();
        builder.Property(x => x.Name).HasMaxLength(3000).IsRequired();
        builder.Property(x => x.Price).IsRequired();
        builder.Property(x => x.Weight).IsRequired();
        builder.Property(x => x.Unit).IsRequired().HasMaxLength(50);

        builder.HasMany(x => x.ItemOptions).WithMany(x => x.Items);

        builder.HasIndex(x => x.Name);
        builder.HasIndex(x => x.Price);
        builder.HasIndex(x => x.Weight);
    }
}