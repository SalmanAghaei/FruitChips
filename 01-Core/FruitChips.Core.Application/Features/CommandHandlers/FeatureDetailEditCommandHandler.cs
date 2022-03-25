using Mapster;
using Core.Application.Commands;
using I6.Utilities.Services.Commands;
using FruitChips.Core.Contracts.Features.Commands;
using FruitChips.Core.Contracts.Features.Repositories;

namespace FruitChips.Core.Application.Features.CommandHandlers;

    public class FeatureDetailEditCommandHandler : CommandHandler<FeatureDetailEditCommand>
    {
        private readonly IFeatureDetailCommandRepository _featureDetailCommandRepository;
        public FeatureDetailEditCommandHandler(IFeatureDetailCommandRepository featureDetailCommandRepository)
        {
            _featureDetailCommandRepository = featureDetailCommandRepository;
        }
        public override Task<CommandResult> Handle(FeatureDetailEditCommand request)
        {
            var featureDetail = _featureDetailCommandRepository.Get(request.Id);
            if (featureDetail is null)
                return ResultAsync(System.Net.HttpStatusCode.NotFound);
            request.Adapt(featureDetail);
            _featureDetailCommandRepository.Commit();
            return OkAsync();
        }

    }
