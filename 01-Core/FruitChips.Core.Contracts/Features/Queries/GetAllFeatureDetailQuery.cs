using Core.Application.Queries;
using Persistance.SqlData.Quries;
using FruitChips.Core.Contracts.Features.Dtos;
using FruitChips.Core.Contracts.Features.Filters;

namespace FruitChips.Core.Contracts.Features.Queries
{
    public class GetAllFeatureDetailQuery : PageQuery<PagedData<FeatureDetailListDto>>, IFeatureDetailFilter
    {
        public int FeatureId { get ; set ; }
    }
}
