using Persistance.SqlData.Commands;
using FruitChips.Persistance.SqlData.Context;
using FruitChips.Core.Domain.Products.Entities;
using FruitChips.Core.Contracts.Products.Repositories;

namespace FruitChips.Persistance.SqlData.Products.Repositories
{
    public class ProductCommandRepository : CommandRepository<Product, int, CommandDbContext>, IProductCommandRepository
    {
        public ProductCommandRepository(CommandDbContext dbContext) : base(dbContext)
        {
        }


    }
}
