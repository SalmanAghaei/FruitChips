using Core.Application.Commands;
using I6.Utilities.Services.Commands;

namespace FruitChips.Core.Contracts.Products.Commands
{
    public class ProductCreateCommand:ICommand
    {
        public string SKU { get; set; }
        public string Name { get; set; }
        public int?[] CategoryIds { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
    }
}
