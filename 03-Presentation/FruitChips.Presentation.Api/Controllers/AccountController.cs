using Presentation.Api;
using Microsoft.AspNetCore.Mvc;
using FruitChips.Core.Contracts.Identity;
using Microsoft.AspNetCore.Authorization;
using FruitChips.Core.Contracts.Identity.Dtos;

namespace FruitChips.Presentation.Api.Controllers
{
    public class AccountController : BaseContoller
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpPost("get-token")]
        //[ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(SignInDto request)
        {
            return Ok(await _accountService.Login(request));
        }

        [HttpPost("logout")]
        public IActionResult LogOut(LogOutDto logOutDto)
        {
            return Ok(_accountService.LogOut(logOutDto));
        }

      
    }
}
