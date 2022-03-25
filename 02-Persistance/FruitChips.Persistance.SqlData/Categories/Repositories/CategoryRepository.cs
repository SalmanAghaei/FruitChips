using FruitChips.Core.Contracts.Features.Repositories;
using FruitChips.Core.Domain.Categories.Entities;
using FruitChips.Persistance.SqlData.Context;
using Persistance.SqlData.Commands;

namespace FruitChips.Persistance.SqlData.Categories.Repositories
{
    public class CategoryCommandRepository : CommandRepository<Category, int, CommandDbContext>, ICategoryCommandRepository
    {
        public CategoryCommandRepository(CommandDbContext dbContext) : base(dbContext)
        {
        }
    }
}
