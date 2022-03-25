using FruitChips.Core.Contracts.Categories.Commands;
using Microsoft.AspNetCore.Mvc;
using Presentation.Api;

namespace FruitChips.Presentation.Api.Controllers
{
    public class CategoryController : BaseContoller
    {
        [HttpPost("add")]
        public async Task<IActionResult> Create([FromBody] CategoryCreateCommand command)
        {
            return await Create<CategoryCreateCommand>(command);
        }
        [HttpPut("edit")]
        public async Task<IActionResult> Edit([FromBody] CategoryEditCommand command)
        {
            return await Edit<CategoryEditCommand>(command);
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> Remove([FromBody] CategoryDeleteCommand command)
        {
            return await Delete<CategoryDeleteCommand>(command);
        }
    }
}
