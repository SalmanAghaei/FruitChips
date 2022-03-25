using Microsoft.EntityFrameworkCore;
using FruitChips.Core.Domain.Products.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FruitChips.Persistance.SqlData.Products.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(100).IsUnicode();
            builder.Property(x => x.Description).HasMaxLength(1000).IsUnicode();
            builder.Property(x => x.ShortDescription).HasMaxLength(500).IsUnicode();
            builder.HasIndex(x => new {x.IsDeleted,x.SKU}).HasName("IX_Products_SKU_IsDeleted").IsUnique();
            builder.Property(x => x.SKU)
            .IsRequired()
            .HasMaxLength(10)
            .IsUnicode(false);
        }
    }

    public class CategoryProductConfiguration : IEntityTypeConfiguration<CategoryProduct>
    {
        public void Configure(EntityTypeBuilder<CategoryProduct> builder)
        {
            builder.HasKey("CategoryId", "ProductId");
        }
    }
}
