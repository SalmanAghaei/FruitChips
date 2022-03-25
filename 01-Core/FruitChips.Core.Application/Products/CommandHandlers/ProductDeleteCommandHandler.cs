using Core.Application.Commands;
using FruitChips.Core.Contracts.Products.Commands;
using FruitChips.Core.Contracts.Products.Repositories;
using I6.Utilities.Services.Commands;

namespace FruitChips.Core.Application.Products.CommandHandlers
{
    public class ProductDeleteCommandHandler : CommandHandler<ProductDeleteCommand>
    {
        private readonly IProductCommandRepository _productCommandRepository;
        public ProductDeleteCommandHandler(IProductCommandRepository productCommandRepository)
        {
            _productCommandRepository = productCommandRepository;
        }
        public override Task<CommandResult> Handle(ProductDeleteCommand request)
        {
            if (!_productCommandRepository.Exists(x => x.Id == request.ProductId))
                return FailedAsync("Product Not Found!!");
            _productCommandRepository.Delete(request.ProductId);
            _productCommandRepository.Commit();
            return OkAsync();
        }
    }
}
