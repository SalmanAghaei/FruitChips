using Core.Application.Commands;
using I6.Utilities.Services.Commands;
using FruitChips.Core.Contracts.Features.Repositories;
using FruitChips.Core.Contracts.Features.Commands;

namespace FruitChips.Core.Application.Features.CommandHandlers
{
    public class FeatureRemoveCommandHandler : CommandHandler<FeatureRemoveCommand>
    {
        private readonly IFeatureCommandRepository _featureCommandRepository;

        public FeatureRemoveCommandHandler(IFeatureCommandRepository featureCommandRepository)
        {
            _featureCommandRepository = featureCommandRepository;
        }
        public override Task<CommandResult> Handle(FeatureRemoveCommand request)
        {
            ArgumentNullException.ThrowIfNull(request);
            var feature = _featureCommandRepository.Get(request.Id);
            if (feature is null)
                return ResultAsync(System.Net.HttpStatusCode.NotFound);
            else
            {
                _featureCommandRepository.Delete(request.Id);
                _featureCommandRepository.Commit();
            }
            return OkAsync();
        }
    }
}
