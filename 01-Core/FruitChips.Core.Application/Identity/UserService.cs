using Core.Domain;
using FruitChips.Core.Contracts.Identity;
using FruitChips.Core.Domain.Customers.Entities;
using FruitChips.Core.Domain.Identity.Entities;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Utilities;
using Utilities.Extensions;
using FruitChips.Core.Contracts.Identity.Dtos;

namespace FruitChips.Core.Application.Identity
{

    public class UserService : IUserService
    {

        private readonly UserManager<Customer> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public UserService(UserManager<Customer> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<ApiResult> CreateUserAsync(AddUserDto addUserDto)
        {
            var user = addUserDto.Adapt<Customer>();
            var result = await _userManager.CreateAsync(user);
            if (result.Succeeded)
                return ApiResultHandler.Ok();
            return ApiResultHandler.Failed(result.JoinIdentityErrors());

        }

        public async Task<ApiResult> DeleteUserAsync(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user is null)
                return ApiResultHandler.Failed("", System.Net.HttpStatusCode.NotFound);
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
                return ApiResultHandler.Ok();
            return ApiResultHandler.Failed(result.JoinIdentityErrors());

        }

        public async Task<ApiResult> EditUserAsync(EditUserDto editUserDto)
        {
            var user = await _userManager.FindByIdAsync(editUserDto.Id.ToString());
            if (user is null)
                return ApiResultHandler.Failed("", System.Net.HttpStatusCode.NotFound);
            user.Email = editUserDto.Email;
            user.LastName = editUserDto.LastName;
            user.FirstName = editUserDto.FirstName;
            user.PhoneNumber = editUserDto.PhoneNumber;
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
                return ApiResultHandler.Ok();
            return ApiResultHandler.Failed(result.JoinIdentityErrors());

        }
        public async Task<ApiResult> AddUserToRoleAsync(AddUserToRoleDto addUserToRole)
        {

            var user = await _userManager.FindByIdAsync(addUserToRole.UserId.ToString());
            var userRoles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, userRoles);
            if (result.Succeeded)
            {
                var resu = await _userManager.AddToRolesAsync(user, addUserToRole.RoleNames);
                if (resu.Succeeded)
                    return ApiResultHandler.Ok();
                return ApiResultHandler.Failed(result.JoinIdentityErrors());
            }
            else
                return ApiResultHandler.Failed(result.JoinIdentityErrors());
        }

        public async  Task<ApiResult<List<UserListDto>>> GetUsersAsync()
        {
            var result =await _userManager.Users.AsNoTracking().Select(x => new UserListDto
            {
                Email = x.Email,
                FirstName = x.FirstName,
                LastName = x.LastName,
                PhoneNumber = x.PhoneNumber,
                UserId = x.Id,
                UserName = x.UserName
            }).ToListAsync();
            return ApiResultHandler<List<UserListDto>>.Ok(result);
        }

        public async Task<ApiResult<List<UserRoleList>>> GetUserRolesAsync(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var result = await _userManager.GetRolesAsync(user);
            var q=_roleManager.Roles.Where(x => result.Contains(x.Name)).Select(x=>new UserRoleList
            {
                RoleId = x.Id,
                RoleName=x.Name
            });
            return ApiResultHandler<List<UserRoleList>>.Ok(q.ToList());
        }
    }
}
