using FruitChips.Core.Domain.Features.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FruitChips.Persistance.SqlData.Features.Configurations
{
    public class FeatureConfig : IEntityTypeConfiguration<Feature>
    {
        public void Configure(EntityTypeBuilder<Feature> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(256);
        }
    }
    public class FeatureDetailConfig : IEntityTypeConfiguration<FeatureDetail>
    {
        public void Configure(EntityTypeBuilder<FeatureDetail> builder)
        {
            builder.Property(x => x.Value).HasMaxLength(256);
        }
    }
}
