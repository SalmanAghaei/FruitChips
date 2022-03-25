using Core.Domain.Entities;
using FruitChips.Core.Domain.Categories.Entities;
using FruitChips.Core.Domain.Features.Entities;

namespace FruitChips.Core.Domain.Products.Entities
{
    public class Product : BaseEntity<int>
    {
        public string SKU { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Feature> Features { get; set; }
        public virtual ICollection<CategoryProduct> CategoryProducts { get; set; }
        public virtual ICollection<Inventory> Inventory { get; set; }
    }
}
