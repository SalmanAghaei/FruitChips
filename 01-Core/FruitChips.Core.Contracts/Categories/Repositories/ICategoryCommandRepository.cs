using Core.Contracts.Data.Commands;
using FruitChips.Core.Domain.Categories.Entities;

namespace FruitChips.Core.Contracts.Features.Repositories
{
    public interface ICategoryCommandRepository : ICommandRepository<Category, int>
    {
    }
}
