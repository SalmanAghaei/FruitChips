using FruitChips.Core.Domain.Identity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FruitChips.Persistance.Identity.SqlData.Configurations
{
    public class AppTokenConfig : IEntityTypeConfiguration<AppToken>
    {
        public void Configure(EntityTypeBuilder<AppToken> builder)
        {
            builder.Property(x => x.AccessTokenHash).HasColumnType("Nvarchar(3000)");
        }
    }
}
