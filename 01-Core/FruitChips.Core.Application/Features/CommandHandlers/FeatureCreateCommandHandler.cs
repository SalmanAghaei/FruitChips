using Mapster;
using Core.Application.Commands;
using I6.Utilities.Services.Commands;
using FruitChips.Core.Domain.Features.Entities;
using FruitChips.Core.Contracts.Features.Commands;
using FruitChips.Core.Contracts.Features.Repositories;

namespace FruitChips.Core.Application.Features.CommandHandlers;
    public class FeatureCreateCommandHandler : CommandHandler<FeatureCreateCommand>
    {
        private readonly IFeatureCommandRepository _featureCommandRepository;

        public FeatureCreateCommandHandler(IFeatureCommandRepository featureCommandRepository)
        {
            _featureCommandRepository = featureCommandRepository;
        }

        public override Task<CommandResult> Handle(FeatureCreateCommand request)
        {
            ArgumentNullException.ThrowIfNull(request);
            _featureCommandRepository.Insert(request.Adapt<Feature>());
            _featureCommandRepository.Commit();
            return OkAsync();
        }
    }
