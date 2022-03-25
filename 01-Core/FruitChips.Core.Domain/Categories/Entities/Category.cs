using Core.Domain.Entities;
using FruitChips.Core.Domain.Products.Entities;

namespace FruitChips.Core.Domain.Categories.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; } 
        public virtual ICollection<CategoryProduct> CategoryProducts { get; set; }
    }
}
