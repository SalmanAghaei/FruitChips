using Core.Contracts;
using Core.Domain;
using FruitChips.Core.Domain.Identity.Entities;
using FruitChips.Core.Contracts.Identity.Dtos;

namespace FruitChips.Core.Contracts.Identity
{
    public interface ITokenService: IScopeLifeTime
    {
        Task<ApiResult<string>> GenerateTokenAsync(GenerateTokenDto tokenDto);
        void AddUserToken(AppToken userToken);
        void DeleteToken(Guid userId);
        bool ExistToken(string token);
        bool UserSecurityStampValid(Guid userId, string securityStamp);
    }
}
