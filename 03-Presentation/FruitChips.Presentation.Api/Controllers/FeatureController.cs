using Presentation.Api;
using Microsoft.AspNetCore.Mvc;
using Persistance.SqlData.Quries;
using FruitChips.Core.Contracts.Features.Dtos;
using FruitChips.Core.Contracts.Features.Queries;
using FruitChips.Core.Contracts.Features.Commands;

namespace FruitChips.Presentation.Api.Controllers
{
    public class FeatureController : BaseContoller
    {
        public FeatureController()
        {

        }
        [HttpPost("add")]
        public async Task<IActionResult> Create([FromBody] FeatureCreateCommand command)
        {
            return await Create<FeatureCreateCommand>(command);
        }
        [HttpPut("edit")]
        public async Task<IActionResult> Edit([FromBody] FeatureEditCommand command)
        {
            return await Edit<FeatureEditCommand>(command);
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> Remove([FromBody] FeatureRemoveCommand command)
        {
            return await Delete<FeatureRemoveCommand>(command);
        }
        [HttpPost("get-features")]
        public async Task<IActionResult> Get([FromBody] GetAllFeatureQuery query)
        {
            return await Query<GetAllFeatureQuery, PagedData<FeatureListDto>>(query);
        }

        [HttpPost("get-feature-details")]
        public async Task<IActionResult> GetFeatureDetails([FromBody] GetAllFeatureDetailQuery query)
        {
            return await Query<GetAllFeatureDetailQuery, PagedData<FeatureDetailListDto>>(query);
        }
    }
}
