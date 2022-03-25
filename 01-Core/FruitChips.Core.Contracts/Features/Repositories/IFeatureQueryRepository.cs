using Core.Contracts.Data.Queries;
using FruitChips.Core.Contracts.Features.Dtos;
using FruitChips.Core.Contracts.Features.Filters;

namespace FruitChips.Core.Contracts.Features.Repositories
{
    public interface IFeatureQueryRepository: IQueryRepository
    {
        Task<(List<FeatureListDto> list, int totalCount)> GetAllAsync(IFeatureFilter filter);

        Task<(List<FeatureDetailListDto> list, int totalCount)> GetAllFeatureDetailAsync(IFeatureDetailFilter filter);
    }
}
