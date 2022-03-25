using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistance.SqlData.Models;

namespace Persistance.SqlData.Configurations
{
    public class AuditConfig : IEntityTypeConfiguration<Audit>
    {
        public void Configure(EntityTypeBuilder<Audit> builder)
        {
            builder.Property(x => x.NewValues);
            builder.Property(x => x.OldValues);
            builder.Property(x => x.TableName).HasMaxLength(100);
            builder.Property(x => x.Type).HasMaxLength(10);
            builder.Property(x => x.PrimaryKey).HasMaxLength(100);
            builder.Property(x => x.AffectedColumns).HasMaxLength(10000);
        }
    }
}
