using Core.Contracts;
using Core.Domain;
using FruitChips.Core.Contracts.Identity.Dtos;

namespace FruitChips.Core.Contracts.Identity
{
    public interface IRoleService : IScopeLifeTime
    {
        Task<ApiResult> CreateAsync(RoleCreateDto roleCreateDto);
        Task<ApiResult> EditAsync(RoleEditDto roleEditDto);
        Task<ApiResult> DeleteAsync(RoleDeleteDto roleDeleteDto);
        Task<ApiResult<List<RolesListDto>>> GetRoelsAsync();
    }
}
