using Core.Domain.Entities;
using FruitChips.Core.Domain.Categories.Entities;

namespace FruitChips.Core.Domain.Products.Entities
{
    public class CategoryProduct:BaseEntity
    {
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
