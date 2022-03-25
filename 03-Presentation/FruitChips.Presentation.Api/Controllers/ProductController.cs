using FruitChips.Core.Contracts.Products.Commands;
using FruitChips.Core.Contracts.Products.Queries;
using Microsoft.AspNetCore.Mvc;
using Persistance.SqlData.Quries;
using Presentation.Api;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using FruitChips.Core.Contracts.Products.Dtos;

namespace FruitChips.Presentation.Api.Controllers
{
    public class ProductController : BaseContoller
    {
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] ProductCreateCommand command)
        {
            return await Create(command);
        }

        [HttpPut("edit")]
        public async Task<IActionResult> Edit([FromBody] ProductEditCommand command)
        {
            return await Edit<ProductEditCommand>(command);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromBody] ProductDeleteCommand command)
        {
            return await Delete<ProductDeleteCommand>(command);
        }

        [HttpPost("get-all")]
        public async Task<IActionResult> Get([FromBody] AllProductQuery query)
        {
            return await Query< AllProductQuery,PagedData<ProductsListDto>>(query);
        }
    }
}
