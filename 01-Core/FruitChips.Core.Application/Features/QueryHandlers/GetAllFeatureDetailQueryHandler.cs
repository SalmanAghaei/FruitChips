using Core.Application.Queries;
using FruitChips.Core.Contracts.Features.Dtos;
using FruitChips.Core.Contracts.Features.Queries;
using FruitChips.Core.Contracts.Features.Repositories;
using Persistance.SqlData.Quries;

namespace FruitChips.Core.Application.Features.QueryHandlers
{
    public class GetAllFeatureDetailQueryHandler : QueryHandler<GetAllFeatureDetailQuery, PagedData<FeatureDetailListDto>>
    {
        private readonly IFeatureQueryRepository _featureQueryRepository;

        public GetAllFeatureDetailQueryHandler(IFeatureQueryRepository featureQueryRepository)
        {
            _featureQueryRepository = featureQueryRepository;
        }

        public override async Task<QueryResult<PagedData<FeatureDetailListDto>>> Handle(GetAllFeatureDetailQuery request)
        {
            var result = await _featureQueryRepository.GetAllFeatureDetailAsync(request);
            var returnValue = new PagedData<FeatureDetailListDto>(request)
            {
                QueryResult = result.list,
                TotalCount = result.totalCount
            };
            return Result(returnValue);
        }
    }
}
