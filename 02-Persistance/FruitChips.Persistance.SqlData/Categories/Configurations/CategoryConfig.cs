using FruitChips.Core.Domain.Categories.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FruitChips.Persistance.SqlData.Categories.Configurations;

internal class CategoryConfig : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.Property(x => x.Name).HasColumnType("NvarChar(256)").IsRequired();
        builder.Property(x => x.Description).HasColumnType("NvarChar(500)");
    }
}
