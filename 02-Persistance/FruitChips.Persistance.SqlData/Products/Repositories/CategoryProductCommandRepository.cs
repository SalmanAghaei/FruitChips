using Persistance.SqlData.Commands;
using FruitChips.Persistance.SqlData.Context;
using FruitChips.Core.Domain.Products.Entities;
using FruitChips.Core.Contracts.Products.Repositories;

namespace FruitChips.Persistance.SqlData.Products.Repositories
{
    public class CategoryProductCommandRepository : CommandRepository<CategoryProduct, int, CommandDbContext>, ICategoryProductCommandRepository
    {
        public CategoryProductCommandRepository(CommandDbContext dbContext) : base(dbContext)
        {
        }

        private IList<CategoryProduct> GetProductCategories(int productId) =>
            _dbContext.CategoryProduct.Where(x => x.ProductId == productId).ToList();

        public void InsertCategoryProduct(int?[] categoryIds, int productId)
        {
            RemoveProductCategories(productId);
            if (categoryIds is null)
                return;
            List<CategoryProduct> categoryProducts = new();
            categoryProducts.AddRange(categoryIds.Select(categoryId => new CategoryProduct { CategoryId = categoryId.Value, ProductId = productId }));
            _dbContext.CategoryProduct.AddRange(categoryProducts);
        }

        private void RemoveProductCategories(int productId)
        {
            _dbContext.CategoryProduct.RemoveRange(GetProductCategories(productId));
        }
    }
}
