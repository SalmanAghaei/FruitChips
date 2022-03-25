using Core.Application.Commands;

namespace FruitChips.Core.Contracts.Products.Commands
{
    public class ProductDeleteCommand:ICommand
    {
        public int ProductId { get; set; }
    }
}
