using Core.Application.Commands;

namespace FruitChips.Core.Contracts.Products.Commands
{
    public class ProductEditCommand:ICommand
    {
        public int Id { get; set; }
        public string SKU { get; set; }
        public string Name { get; set; }
        public int?[] CategoryIds { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
    }
}
