using Persistance.SqlData.Commands;
using FruitChips.Persistance.SqlData.Context;
using FruitChips.Core.Domain.Features.Entities;
using FruitChips.Core.Contracts.Features.Repositories;

namespace FruitChips.Persistance.SqlData.Features.Repositories
{
    public class FeatureDetailCommandRepository : CommandRepository<FeatureDetail, int, CommandDbContext>, IFeatureDetailCommandRepository
    {
        public FeatureDetailCommandRepository(CommandDbContext dbContext) : base(dbContext)
        {
        }
    }

}
