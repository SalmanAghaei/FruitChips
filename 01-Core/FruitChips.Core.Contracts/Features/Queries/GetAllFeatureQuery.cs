using Core.Application.Queries;
using Persistance.SqlData.Quries;
using FruitChips.Core.Contracts.Features.Dtos;
using FruitChips.Core.Contracts.Features.Filters;

namespace FruitChips.Core.Contracts.Features.Queries
{
    public class GetAllFeatureQuery:PageQuery<PagedData<FeatureListDto>>,IFeatureFilter
    {
    }
}
