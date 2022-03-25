using Core.Contracts.Data.Queries;
using Persistance.SqlData.Context;
using Utilities.DapperService;

namespace Persistance.SqlData.Quries
{
    public class BaseQueryRepository<TDbContext> : IQueryRepository
        where TDbContext : BaseQueryDbContext
    {
        protected readonly TDbContext _dbContext;
        protected readonly IDapper _dapper;
        public BaseQueryRepository(TDbContext dbContext, IDapper dapper)
        {
            _dbContext = dbContext;
            _dapper = dapper;
        }
    }
}
