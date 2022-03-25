using Core.Application.Commands;

namespace FruitChips.Core.Contracts.Features.Commands
{
    public  class FeatureCreateCommand:ICommand
    {
        public string Name { get; set; }
    }
}
