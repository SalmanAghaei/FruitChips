using Core.Application.Commands;
using FruitChips.Core.Contracts.Products.Commands;
using FruitChips.Core.Contracts.Products.Repositories;
using FruitChips.Core.Domain.Products.Entities;
using I6.Utilities.Services.Commands;
using Mapster;

namespace FruitChips.Core.Application.Products.CommandHandlers
{
    public class EditProductCommandHandler : CommandHandler<ProductEditCommand>
    {

        private readonly IProductCommandRepository _productCommandRepository;
        private readonly ICategoryProductCommandRepository _categoryProductCommandRepository;
        public EditProductCommandHandler(IProductCommandRepository productCommandRepository, ICategoryProductCommandRepository categoryProductCommandRepository)
        {
            _productCommandRepository = productCommandRepository;
            _categoryProductCommandRepository = categoryProductCommandRepository;
        }
        public override async  Task<CommandResult> Handle(ProductEditCommand request)
        {
            if (!await _productCommandRepository.ExistsAsync(x => x.Id == request.Id))
                return Failed("Product Not Found!!");
            if(await _productCommandRepository.ExistsAsync(x=>x.SKU==request.SKU && x.Id != request.Id))
                return Failed($"Sku {request.SKU} Is Exist");
            var product=await _productCommandRepository.GetAsync(request.Id);
            _categoryProductCommandRepository.InsertCategoryProduct(request.CategoryIds, product.Id);
            product.SKU=request.SKU;
            product.Name=request.Name;
            product.Description=request.Description;
            product.ShortDescription = request.ShortDescription;
            await _productCommandRepository.CommitAsync();
            return Ok();

        }
    }
}
