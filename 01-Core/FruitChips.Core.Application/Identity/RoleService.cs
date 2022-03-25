using Utilities;
using Core.Domain;
using Microsoft.AspNetCore.Identity;
using FruitChips.Core.Contracts.Identity;
using FruitChips.Core.Domain.Identity.Entities;
using Microsoft.EntityFrameworkCore;
using Utilities.Extensions;
using FruitChips.Core.Contracts.Identity.Dtos;

namespace FruitChips.Core.Application.Identity
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<AppRole> _roleManager;
        public RoleService(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<ApiResult> CreateAsync(RoleCreateDto roleCreateDto)
        {
            ArgumentNullException.ThrowIfNull(roleCreateDto);
            var result=await _roleManager.CreateAsync(new AppRole { Name=roleCreateDto.Name });
            if (result.Succeeded)
                return ApiResultHandler.Ok();
            return ApiResultHandler.Failed(result.JoinIdentityErrors());
        }

        public async Task<ApiResult> DeleteAsync(RoleDeleteDto roleDeleteDto)
        {
            ArgumentNullException.ThrowIfNull(roleDeleteDto);
            var role=await _roleManager.FindByIdAsync(roleDeleteDto.Id.ToString());
            if (role is not null)
            {
                var result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                    return ApiResultHandler.Ok();
                return ApiResultHandler.Failed(result.JoinIdentityErrors());
            }
            return ApiResultHandler.Failed();
        }

        public async Task<ApiResult> EditAsync(RoleEditDto roleEditDto)
        {
            ArgumentNullException.ThrowIfNull(roleEditDto);
            var role = await _roleManager.FindByIdAsync(roleEditDto.Id.ToString());
            if(role is not null)
            {
                role.Name = roleEditDto.Name;
                var result=await _roleManager.UpdateAsync(role);
                if (result.Succeeded)
                    return ApiResultHandler.Ok();
                return ApiResultHandler.Failed(result.JoinIdentityErrors());
            }
            return ApiResultHandler.Failed();
        }


        public async Task<ApiResult<List<RolesListDto>>> GetRoelsAsync()
        {
            var result=await _roleManager.Roles.AsNoTracking().Select(x => new RolesListDto {  Id = x.Id, Name = x.Name }).ToListAsync();
            return ApiResultHandler<List<RolesListDto>>.Ok(result);
        }
    }
}
