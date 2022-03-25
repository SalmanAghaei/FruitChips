using Core.Contracts;
using Core.Domain;
using FruitChips.Core.Contracts.Identity.Dtos;

namespace FruitChips.Core.Contracts.Identity
{
    public interface IAccountService: IScopeLifeTime
    {
        Task<ApiResult<RsponseTokenInfo>> Login(SignInDto dto);

        ApiResult LogOut(LogOutDto dto);
    }
}
