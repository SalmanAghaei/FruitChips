using Core.Application.Queries;
using Persistance.SqlData.Quries;
using FruitChips.Core.Contracts.Products.Dtos;

namespace FruitChips.Core.Contracts.Products.Queries
{
    public interface IAllProductFiletr : IPageQuery
    {

    }

    public class AllProductValidator : PageQueryValidator<AllProductQuery>
    {
    }
    public class AllProductQuery : PageQuery<PagedData<ProductsListDto>>, IAllProductFiletr
    {
        public string ProductName { get; set; }
    }
}
