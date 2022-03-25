using FruitChips.Core.Contracts.Identity;
using Microsoft.AspNetCore.Mvc;
using FruitChips.Core.Contracts.Identity.Dtos;

namespace FruitChips.Presentation.Api.Controllers
{
    public class UserController : Controller
    {

        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("create-user")]

        public async Task<IActionResult> CerateUser(AddUserDto userDto)
        {
            var result = await _userService.CreateUserAsync(userDto);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }


        [HttpPut("edit-user")]
        public async Task<IActionResult> EditUser(EditUserDto userDto)
        {
            var result = await _userService.EditUserAsync(userDto);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpDelete("delete-user")]
        public async Task<IActionResult> EditUser(Guid userId)
        {
            var result = await _userService.DeleteUserAsync(userId);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPost("add-user-to-role")]
        public async Task<IActionResult> AddUserToRole([FromBody]AddUserToRoleDto userDto)
        {
            var result = await _userService.AddUserToRoleAsync(userDto);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpGet("get-users")]
        public async Task<IActionResult> GetUsers()
        {
            var result = await _userService.GetUsersAsync();
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpGet("get-user-roles")]
        public async Task<IActionResult> GetUserRoles(Guid userId)
        {
            var result=await _userService.GetUserRolesAsync(userId);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
    }
}
