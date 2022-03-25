using FruitChips.Core.Contracts.Identity;
using Microsoft.AspNetCore.Mvc;
using Presentation.Api;
using FruitChips.Core.Contracts.Identity.Dtos;

namespace FruitChips.Presentation.Api.Controllers
{
    public class RoleController : BaseContoller
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RoleCreateDto roleCreateDto)
        {
            var result =await _roleService.CreateAsync(roleCreateDto);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] RoleEditDto roleEditDto)
        {
            var result = await _roleService.EditAsync(roleEditDto);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] RoleDeleteDto roleDeleteDto)
        {
            var result = await _roleService.DeleteAsync(roleDeleteDto);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpGet]
        public  async Task<IActionResult> GetRoles()
        {
            var result =await  _roleService.GetRoelsAsync();
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
    }
}
