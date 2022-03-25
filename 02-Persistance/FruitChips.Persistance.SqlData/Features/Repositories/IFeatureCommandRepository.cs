using Persistance.SqlData.Commands;
using FruitChips.Persistance.SqlData.Context;
using FruitChips.Core.Domain.Features.Entities;
using FruitChips.Core.Contracts.Features.Repositories;

namespace FruitChips.Persistance.SqlData.Features.Repositories
{
    public class FeatureCommandRepository : CommandRepository<Feature, int, CommandDbContext>, IFeatureCommandRepository
    {
        public FeatureCommandRepository(CommandDbContext dbContext) : base(dbContext)
        {
        }
    }

}
