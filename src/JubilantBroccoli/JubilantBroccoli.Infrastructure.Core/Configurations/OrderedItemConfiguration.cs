using JubilantBroccoli.Domain.Core.Enums;
using JubilantBroccoli.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JubilantBroccoli.Infrastructure.Core.Configurations;

public class OrderedItemConfiguration : IEntityTypeConfiguration<OrderedItem>
{
    public void Configure(EntityTypeBuilder<OrderedItem> builder)
    {
        builder.ToTable("OrderedItems");
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.Status).IsRequired().HasDefaultValue(ItemStatus.Pending);
        builder.Property(x => x.CreatedAt).HasMaxLength(300).IsRequired();
        builder.Property(x => x.UpdatedAt).HasMaxLength(300);
    }
}