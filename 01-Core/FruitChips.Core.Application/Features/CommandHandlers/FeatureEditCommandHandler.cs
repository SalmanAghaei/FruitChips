using Mapster;
using Core.Application.Commands;
using I6.Utilities.Services.Commands;
using FruitChips.Core.Contracts.Features.Repositories;
using FruitChips.Core.Contracts.Features.Commands;

namespace FruitChips.Core.Application.Features.CommandHandlers
{
    public class FeatureEditCommandHandler : CommandHandler<FeatureEditCommand>
    {
        private readonly IFeatureCommandRepository _featureCommandRepository;

        public FeatureEditCommandHandler(IFeatureCommandRepository featureCommandRepository)
        {
            _featureCommandRepository = featureCommandRepository;
        }
        public override Task<CommandResult> Handle(FeatureEditCommand request)
        {
            ArgumentNullException.ThrowIfNull(request);
            var feature = _featureCommandRepository.Get(request.Id);
            if (feature is not null)
                feature = request.Adapt(feature);
            else
                return ResultAsync(System.Net.HttpStatusCode.NotFound);
            _featureCommandRepository.Commit();
            return OkAsync();
        }
    }
}
