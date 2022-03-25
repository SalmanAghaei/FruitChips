using Core.Application.Commands;

namespace FruitChips.Core.Contracts.Features.Commands
{
    public class FeatureEditCommand : ICommand
    {

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
