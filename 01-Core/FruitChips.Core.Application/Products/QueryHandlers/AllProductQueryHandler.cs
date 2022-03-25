using Core.Application.Queries;
using FruitChips.Core.Contracts.Products.Queries;
using Persistance.SqlData.Quries;
using System.Data;
using Utilities.DapperService;
using FruitChips.Core.Contracts.Products.Dtos;

namespace FruitChips.Core.Application.Products.QueryHandlers
{
    public class AllProductQueryHandler : QueryHandler<AllProductQuery, PagedData<ProductsListDto>>
    {

        private readonly IDapper _dapper;
        public AllProductQueryHandler(IDapper dapper)
        {
            _dapper = dapper;
        }
        public override Task<QueryResult<PagedData<ProductsListDto>>> Handle(AllProductQuery request)
        {
            var condition = string.IsNullOrEmpty(request.ProductName) ? "" : $" And Name like '%{request.ProductName}%'";
            var query = request.GetAllQueryGenerate("Products",condition);
            var result=_dapper.GetAll<ProductsListDto>(query, null,request.NeedTotalCount, request.GetTableCount("Products", condition), commandType:CommandType.Text);
            var returnvalue = new PagedData<ProductsListDto>(request)
            {
                QueryResult = result.list,
                TotalCount = result.totalCount
            };
            return ResultAsync(returnvalue);
        }
    }
}
