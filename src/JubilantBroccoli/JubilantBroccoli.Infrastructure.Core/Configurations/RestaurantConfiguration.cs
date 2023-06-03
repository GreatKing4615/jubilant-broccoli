﻿using JubilantBroccoli.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JubilantBroccoli.Infrastructure.Core.Configurations;

public class RestaurantConfiguration : IEntityTypeConfiguration<Restaurant>
{
    public void Configure(EntityTypeBuilder<Restaurant> builder)
    {
        builder.ToTable("Restaurants");
        builder.Property(x => x.Id);
        builder.Property(x => x.Name).HasMaxLength(500).IsRequired();
        builder.Property(x => x.Address).HasMaxLength(500).IsRequired();
        builder.Property(x => x.Closing).HasMaxLength(300).IsRequired();
        builder.Property(x => x.Opening).HasMaxLength(300).IsRequired();
        builder.Ignore(x => x.ItemTypes);

        builder.HasMany(x => x.Orders).WithOne(x => x.Restaurant);
        builder.HasMany(x => x.Items).WithMany(x => x.Restaurants);

        builder.HasIndex(x => x.Name);
    }
}