using FruitChips.Core.Domain.Customers.Entities;
using FruitChips.Core.Domain.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FruitChips.Persistance.Identity.SqlData.Context
{
    public class IdentityContext :
        IdentityDbContext
        <Customer, AppRole, Guid, IdentityUserClaim<Guid>, IdentityUserRole<Guid>, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, AppToken>
    {

        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
            builder.HasDefaultSchema("Auth");
            builder.Entity<Customer>(entity => entity.ToTable(name: "User"));
            builder.Entity<AppRole>(entity => entity.ToTable(name: "Role"));
            builder.Entity<IdentityUserClaim<Guid>>(entity => entity.ToTable(name: "UserClaim"));
            builder.Entity<IdentityUserRole<Guid>>(entity => entity.ToTable(name: "UserRole"));
            builder.Entity<IdentityUserLogin<Guid>>(entity => entity.ToTable(name: "UserLogin"));
            builder.Entity<IdentityRoleClaim<Guid>>(entity => entity.ToTable(name: "UserRoleClaim"));
            builder.Entity<AppToken>(entity => entity.ToTable(name: "UserToken"));
        }
    }
}
