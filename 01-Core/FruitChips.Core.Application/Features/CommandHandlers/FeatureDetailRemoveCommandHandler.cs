using Core.Application.Commands;
using I6.Utilities.Services.Commands;
using FruitChips.Core.Contracts.Features.Commands;
using FruitChips.Core.Contracts.Features.Repositories;

namespace FruitChips.Core.Application.Features.CommandHandlers;
public class FeatureDetailRemoveCommandHandler : CommandHandler<FeatureDetailRemoveCommand>
{
    private readonly IFeatureDetailCommandRepository _featureDetailCommandRepository;
    public FeatureDetailRemoveCommandHandler(IFeatureDetailCommandRepository featureDetailCommandRepository)
    {
        _featureDetailCommandRepository = featureDetailCommandRepository;
    }
    public override Task<CommandResult> Handle(FeatureDetailRemoveCommand request)
    {
        var featureDetail = _featureDetailCommandRepository.Get(request.Id);
        if (featureDetail is null)
            return ResultAsync(System.Net.HttpStatusCode.NotFound);
        _featureDetailCommandRepository.Delete(request.Id);
        _featureDetailCommandRepository.Commit();
        return OkAsync();
    }
}


