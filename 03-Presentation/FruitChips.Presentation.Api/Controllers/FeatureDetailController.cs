using FruitChips.Core.Contracts.Features.Commands;
using Microsoft.AspNetCore.Mvc;
using Presentation.Api;

namespace FruitChips.Presentation.Api.Controllers
{
    public class FeatureDetailController : BaseContoller
    {
        [HttpPost("add")]
        public async Task<IActionResult> Create([FromBody] FeatureDetailCreateCommand command)
        {
            return await Create<FeatureDetailCreateCommand>(command);
        }
        [HttpPut("edit")]
        public async Task<IActionResult> Edit([FromBody] FeatureDetailEditCommand command)
        {
            return await Edit<FeatureDetailEditCommand>(command);
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> Remove([FromBody] FeatureDetailRemoveCommand command)
        {
            return await Delete<FeatureDetailRemoveCommand>(command);
        }
        [HttpPost("get")]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
