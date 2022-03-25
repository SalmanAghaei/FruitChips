using Core.Application.Queries;
using FruitChips.Core.Contracts.Features.Dtos;
using FruitChips.Core.Contracts.Features.Queries;
using FruitChips.Core.Contracts.Features.Repositories;
using Persistance.SqlData.Quries;

namespace FruitChips.Core.Application.Features.QueryHandlers
{
    public class GetAllFeatureQueryHandler : QueryHandler<GetAllFeatureQuery, PagedData<FeatureListDto>>
    {
        private readonly IFeatureQueryRepository _featureQueryRepository;

        public GetAllFeatureQueryHandler(IFeatureQueryRepository featureQueryRepository)
        {
            _featureQueryRepository = featureQueryRepository;
        }

        public override async Task<QueryResult<PagedData<FeatureListDto>>> Handle(GetAllFeatureQuery request)
        {
            var result=await _featureQueryRepository.GetAllAsync(request);
            var returnValue = new PagedData<FeatureListDto>(request)
            {
                QueryResult = result.list,
                TotalCount = result.totalCount
            };
            return Result(returnValue);
        }
    }
}
