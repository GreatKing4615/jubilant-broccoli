using JubilantBroccoli.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JubilantBroccoli.Infrastructure.Core.Configurations;

public class ItemOptionConfiguration : IEntityTypeConfiguration<ItemOption>
{
    public void Configure(EntityTypeBuilder<ItemOption> builder)
    {
        builder.ToTable("ItemOptions");
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.Name).HasMaxLength(300).IsRequired();

        builder.HasIndex(x => x.Name);
    }
}