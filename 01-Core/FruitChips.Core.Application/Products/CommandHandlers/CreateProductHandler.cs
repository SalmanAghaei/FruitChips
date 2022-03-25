using Core.Application.Commands;
using FruitChips.Core.Contracts.Products.Commands;
using FruitChips.Core.Contracts.Products.Repositories;
using FruitChips.Core.Domain.Products.Entities;
using I6.Utilities.Services.Commands;
using Mapster;

namespace FruitChips.Core.Application.Products.CommandHandlers;
public class CreateProductCommandHandler : CommandHandler<ProductCreateCommand>
{
    private readonly IProductCommandRepository _productCommandRepository;

    public CreateProductCommandHandler(IProductCommandRepository productCommandRepository)
    {
        _productCommandRepository = productCommandRepository;
    }
    public override async Task<CommandResult> Handle(ProductCreateCommand request)
    {
        ArgumentNullException.ThrowIfNull(request);
        if (await _productCommandRepository.ExistsAsync(x => x.SKU == request.SKU))
            return Failed($"Sku {request.SKU} Is Exist");

        var product = request.Adapt<Product>();
        SetCategory(request, product);
        _productCommandRepository.Insert(product);
        _productCommandRepository.Commit();
        return Ok();
    }

    private static void SetCategory(ProductCreateCommand request, Product product)
    {
        if (request.CategoryIds is not null)
        {
            var categoryProduct = new List<CategoryProduct>();
            foreach (var categoryId in request.CategoryIds)
            {
                categoryProduct.Add(new CategoryProduct { CategoryId = categoryId.Value, Product = product });
            }
            product.CategoryProducts = categoryProduct;
        }
    }
}
