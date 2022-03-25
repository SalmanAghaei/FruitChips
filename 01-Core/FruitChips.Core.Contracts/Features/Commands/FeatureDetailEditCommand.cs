using Core.Application.Commands;

namespace FruitChips.Core.Contracts.Features.Commands
{
    public class FeatureDetailEditCommand : ICommand
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }
}
