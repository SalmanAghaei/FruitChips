using Core.Domain;
using Microsoft.AspNetCore.Identity;
using FruitChips.Core.Contracts.Identity;
using FruitChips.Core.Domain.Customers.Entities;
using FruitChips.Core.Domain.Identity.Entities;
using FruitChips.Core.Contracts.Identity.Dtos;

namespace FruitChips.Core.Application.Identity
{
    public class AccountService : IAccountService
    {
        private readonly SignInManager<Customer> _signInManager;
        private readonly UserManager<Customer> _userManager;
        private readonly ITokenService _tokenService;

        public AccountService(SignInManager<Customer> signInManager, ITokenService tokenService, UserManager<Customer> userManager)
        {
            _signInManager = signInManager;
            _tokenService = tokenService;
            _userManager = userManager;
        }


        public async Task<ApiResult<RsponseTokenInfo>> Login(SignInDto dto)
        {
            var result = await _signInManager.PasswordSignInAsync(dto.UserName, dto.Password, false, false);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(dto.UserName);
                var tokenResult = await _tokenService.GenerateTokenAsync(new GenerateTokenDto { User = user });
                if (tokenResult.Success)
                {
                    _tokenService.AddUserToken(new AppToken { UserId = user.Id, AccessTokenHash = tokenResult.Data, LoginProvider = "Identity", Name = "JwtToken" });
                    return ApiResultHandler<RsponseTokenInfo>.Ok(new RsponseTokenInfo
                    {
                        Token = tokenResult.Data,
                        UserName = user.UserName,
                        FirstName = user.FirstName,
                        LastName = user.LastName
                    });
                }
                else
                    return ApiResultHandler<RsponseTokenInfo>.Failed(null, "UserName Or Password Incorecct !", System.Net.HttpStatusCode.Unauthorized);
            }
            else
            {
                return ApiResultHandler<RsponseTokenInfo>.Failed(null, "UserName Or Password Incorecct !", System.Net.HttpStatusCode.Unauthorized);
            }
        }

        public ApiResult LogOut(LogOutDto dto)
        {
            _tokenService.DeleteToken(dto.UserId);
            return ApiResultHandler.Ok();
        }
    }
}
