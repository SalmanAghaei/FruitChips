using Core.Application.Commands;

namespace FruitChips.Core.Contracts.Features.Commands
{
    public class FeatureDetailCreateCommand:ICommand
    {
        public int FeatureId { get; set; }
        public string Value { get; set; }

    }
}
