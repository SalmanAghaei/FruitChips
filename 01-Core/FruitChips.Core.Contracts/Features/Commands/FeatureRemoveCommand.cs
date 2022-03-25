using Core.Application.Commands;

namespace FruitChips.Core.Contracts.Features.Commands
{
    public class FeatureRemoveCommand : ICommand
    {
        public int Id { get; set; }
    }
}
