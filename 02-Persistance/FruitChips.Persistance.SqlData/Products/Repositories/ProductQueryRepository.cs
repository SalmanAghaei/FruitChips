using FruitChips.Core.Contracts.Products.Repositories;
using Utilities.DapperService;

namespace FruitChips.Persistance.SqlData.Products.Repositories
{
    public class ProductQueryRepository: IProductQueryRepository
    {
        private readonly IDapper _dapper;
        public ProductQueryRepository(IDapper dapper)
        {
            _dapper = dapper;
        }
    }
}
