using Core.Application.Queries;

namespace FruitChips.Core.Contracts.Features.Filters
{
    public interface IFeatureDetailFilter : IPageQuery
    {

        public int FeatureId { get; set; }
    }
}
