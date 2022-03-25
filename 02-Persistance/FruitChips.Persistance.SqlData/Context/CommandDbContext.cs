using FruitChips.Core.Domain.Features.Entities;
using FruitChips.Core.Domain.Products.Entities;
using Microsoft.EntityFrameworkCore;
using Persistance.SqlData.Context;

namespace FruitChips.Persistance.SqlData.Context
{
    public class CommandDbContext : CommandBaseContext
    {

        public DbSet<Product> Products { get; set; }  
        public DbSet<Inventory> Inventories { get; set; }  
        public DbSet<Feature> Features { get; set; }
        public DbSet<FeatureDetail> FeatureDetails { get; set; }

        public DbSet<CategoryProduct> CategoryProduct { get; set; }
        public CommandDbContext(DbContextOptions<CommandDbContext> dbContext) : base(dbContext)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }

    
}
