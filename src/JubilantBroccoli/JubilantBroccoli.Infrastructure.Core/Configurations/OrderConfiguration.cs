using JubilantBroccoli.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JubilantBroccoli.Infrastructure.Core.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");
        builder.Property(x => x.Id);
        builder.Property(x => x.DeliverType).HasMaxLength(300).IsRequired();
        builder.Property(x => x.DeliveryAddress).HasMaxLength(500).IsRequired(false);
        builder.Property(x => x.Status).HasMaxLength(300).IsRequired();
        builder.Property(x => x.CreatedAt).HasMaxLength(300).IsRequired();
        builder.Property(x => x.UpdatedAt).HasMaxLength(300).IsRequired();

        builder.HasMany(x => x.OrderedItems).WithOne(x => x.Order);
    }
}