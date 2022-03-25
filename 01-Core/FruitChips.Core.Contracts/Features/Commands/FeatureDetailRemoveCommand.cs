using Core.Application.Commands;

namespace FruitChips.Core.Contracts.Features.Commands
{
    public class FeatureDetailRemoveCommand : ICommand
    {
        public int Id { get; set; }
    }
}
