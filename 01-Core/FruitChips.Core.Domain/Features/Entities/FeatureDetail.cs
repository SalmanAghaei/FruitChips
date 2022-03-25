using Core.Domain.Entities;

namespace FruitChips.Core.Domain.Features.Entities
{
    public class FeatureDetail : BaseEntity<int>
    {
        public string Value { get; set; }

        public Feature Feature { get; set; }
        public int FeatureId { get; set; } 


    }
}
