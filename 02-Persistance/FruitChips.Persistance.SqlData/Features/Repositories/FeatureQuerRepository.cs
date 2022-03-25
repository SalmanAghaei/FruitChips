using FruitChips.Core.Contracts.Features.Dtos;
using FruitChips.Core.Contracts.Features.Filters;
using FruitChips.Core.Contracts.Features.Repositories;
using FruitChips.Persistance.SqlData.Context;
using Persistance.SqlData.Quries;
using Utilities.DapperService;

namespace FruitChips.Persistance.SqlData.Features.Repositories
{
    public class FeatureQuerRepository : BaseQueryRepository<QueryDbContext>, IFeatureQueryRepository
    {
        public FeatureQuerRepository(QueryDbContext dbContext, IDapper dapper) : base(dbContext, dapper)
        {
        }

        public async Task<(List<FeatureListDto> list, int totalCount)> GetAllAsync(IFeatureFilter filter)
        {
            var query = filter.GetAllQueryGenerate("Features", "");
            var result = await _dapper.GetAllAsync<FeatureListDto>(query, null, filter.NeedTotalCount, filter.GetTableCount("Features", ""));
            return result;
        }

        public async Task<(List<FeatureDetailListDto> list, int totalCount)> GetAllFeatureDetailAsync(IFeatureDetailFilter filter)
        {
            var condition = $" And FeatureId ={filter.FeatureId}";
            var query = filter.GetAllQueryGenerate("FeatureDetails", condition);
            var result = await _dapper.GetAllAsync<FeatureDetailListDto>(query, null, filter.NeedTotalCount, filter.GetTableCount("FeatureDetails", condition));
            return result;
        }
    }
}
