using FruitChips.Core.Domain.Customers.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FruitChips.Persistance.Identity.SqlData.Configurations
{

    public class CustomerConfig : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.Property(x => x.FirstName).HasColumnType("Nvarchar(256)");
            builder.Property(x => x.LastName).HasColumnType("Nvarchar(256)");
        }
    }
}
