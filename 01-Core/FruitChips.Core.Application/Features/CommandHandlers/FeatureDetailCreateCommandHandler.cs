using Mapster;
using Core.Application.Commands;
using I6.Utilities.Services.Commands;
using FruitChips.Core.Domain.Features.Entities;
using FruitChips.Core.Contracts.Features.Commands;
using FruitChips.Core.Contracts.Features.Repositories;

namespace FruitChips.Core.Application.Features.CommandHandlers
{
    public partial class FeatureDetailCreateCommandHandler : CommandHandler<FeatureDetailCreateCommand>
    {
        private readonly IFeatureDetailCommandRepository _featureDetailCommandRepository;
        public FeatureDetailCreateCommandHandler(IFeatureDetailCommandRepository featureDetailCommandRepository)
        {
            _featureDetailCommandRepository = featureDetailCommandRepository;
        }
        public override Task<CommandResult> Handle(FeatureDetailCreateCommand request)
        {
            _featureDetailCommandRepository.Insert(request.Adapt<FeatureDetail>());
            _featureDetailCommandRepository.Commit();
            return OkAsync();
        }
    }
}
