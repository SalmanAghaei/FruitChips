using Core.Contracts;
using Core.Contracts.Data.Commands;
using FruitChips.Core.Domain.Features.Entities;

namespace FruitChips.Core.Contracts.Features.Repositories
{
    public interface IFeatureCommandRepository : ICommandRepository<Feature, int>,IScopeLifeTime
    {
    }
}
