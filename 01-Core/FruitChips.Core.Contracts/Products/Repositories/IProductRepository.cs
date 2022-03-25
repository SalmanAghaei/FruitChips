using Core.Contracts.Data.Commands;
using FruitChips.Core.Domain.Products.Entities;

namespace FruitChips.Core.Contracts.Products.Repositories
{
    public interface IProductCommandRepository : ICommandRepository<Product, int>
    {

    }

    public interface ICategoryProductCommandRepository : ICommandRepository<CategoryProduct, int>
    {
        void InsertCategoryProduct(int?[] categoryIds, int productId);
    }
}
