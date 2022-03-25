using Core.Contracts;
using Core.Domain;
using FruitChips.Core.Contracts.Identity.Dtos;

namespace FruitChips.Core.Contracts.Identity
{
    public interface IUserService : IScopeLifeTime
    {

        Task<ApiResult> CreateUserAsync(AddUserDto addUserDto);
        Task<ApiResult> EditUserAsync(EditUserDto editUserDto);
        Task<ApiResult> DeleteUserAsync(Guid id);
        Task<ApiResult> AddUserToRoleAsync(AddUserToRoleDto addUserToRole);
        Task<ApiResult<List<UserListDto>>> GetUsersAsync();
        Task<ApiResult<List<UserRoleList>>> GetUserRolesAsync(Guid userId);
    }
}
