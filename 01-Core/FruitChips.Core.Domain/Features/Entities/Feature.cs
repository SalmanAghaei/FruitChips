using Core.Domain.Entities;
using FruitChips.Core.Domain.Products.Entities;

namespace FruitChips.Core.Domain.Features.Entities
{
    public class Feature : BaseEntity<int>
    {
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public virtual ICollection<FeatureDetail> FeatureDetails { get; set; }

    }
}
