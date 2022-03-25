using Utilities;
using Core.Domain;
using System.Text;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using FruitChips.Core.Contracts.Identity;
using FruitChips.Core.Domain.Identity.Entities;
using FruitChips.Core.Domain.Customers.Entities;
using FruitChips.Core.Contracts.Identity.Dtos;

namespace FruitChips.Core.Application.Identity
{
    public class TokenService : ITokenService
    {
        private readonly AppSettings _appSettings;
        private readonly ISecurityService _securityService;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<Customer> _userManager;
        private readonly IUserTokenRepository _userTokenRepository;
        public TokenService(
            AppSettings appSettings,
            ISecurityService securityService,
            RoleManager<AppRole> roleManager,
            UserManager<Customer> userManager,
            IUserTokenRepository userTokenRepository
            )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _appSettings = appSettings;
            _userTokenRepository = userTokenRepository;
            _securityService = securityService;
        }
        public async Task<ApiResult<string>> GenerateTokenAsync(GenerateTokenDto tokenDto)
        {
         
            var userClaims =await _userManager.GetClaimsAsync(tokenDto.User);
            var roles=await _userManager.GetRolesAsync(tokenDto.User);
            var roleIds = GetRoleId(roles.ToList());

            var secretkey = Encoding.UTF8.GetBytes(_appSettings.Jwt.Key);
            var signInCredentials = new SigningCredentials(new SymmetricSecurityKey(secretkey), SecurityAlgorithms.HmacSha256Signature);

            var encryptionKey = Encoding.UTF8.GetBytes(_appSettings.Jwt.EncryptKey);
            var encryptingCredentials = 
                new EncryptingCredentials(new SymmetricSecurityKey(encryptionKey), SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);


            IEnumerable<Claim> claims = SetClaims(tokenDto.User, userClaims.ToList(), roleIds);



            var descriptor = new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Jwt.Issuer,
                Audience = _appSettings.Jwt.Audience,
                Expires = DateTime.UtcNow.AddMinutes(_appSettings.Jwt.DurationInMinutes),
                EncryptingCredentials = encryptingCredentials,
                SigningCredentials = signInCredentials,
                Subject = new ClaimsIdentity(claims)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(descriptor);
            string encryptedJwt = tokenHandler.WriteToken(securityToken);

            return ApiResultHandler<string>.Ok(encryptedJwt);
        }

        private static IEnumerable<Claim> SetClaims(Customer user, IEnumerable<Claim> userClaims, IList<Guid> roles)
        {
            var roleClaims = new List<Claim>();
            SetRolesClaim(roles, roleClaims);

            IEnumerable<Claim> claims = SetAllClaims(user, userClaims, roleClaims);
            return claims;
        }
        private static void SetRolesClaim(IList<Guid> roles, List<Claim> roleClaims)
        {
            foreach (var role in roles)
            {
                roleClaims.Add(new Claim("roles", role.ToString()));
            }
        }
        private static IEnumerable<Claim> SetAllClaims(Customer user, IEnumerable<Claim> userClaims, List<Claim> roleClaims)
        {
            return new[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(new ClaimsIdentityOptions().SecurityStampClaimType,user.SecurityStamp)

            }
                   .Union(userClaims)
                   .Union(roleClaims);
        }
        private List<Guid> GetRoleId(List<string> roleNames)
        {
            List<Guid> roleIds=new List<Guid>();
            foreach(var roleName in roleNames)
            {
                roleIds.Add(_roleManager.Roles.Where(x => x.Name == roleName).FirstOrDefault().Id);
            }
            return roleIds;
        }

        public void DeleteToken(Guid userId)
        {
            _userTokenRepository.RemoveToken(userId);
            _userTokenRepository.SaveChanges();
        }


        public void AddUserToken(AppToken userToken)
        {
            InvalidateUserTokensAsync(userToken.UserId);
            userToken.AccessTokenHash=_securityService.GetSha256Hash(userToken.AccessTokenHash);
            _userTokenRepository.SaveToken(userToken);
            _userTokenRepository.SaveChanges();
        }

        void InvalidateUserTokensAsync(Guid userId)
        {
            _userTokenRepository.RemoveToken(userId);
        }


        public bool ExistToken(string token)
        {
            var tokenHash = _securityService.GetSha256Hash(token);
            return _userTokenRepository.ExistToken(tokenHash);
        }


        public bool UserSecurityStampValid(Guid userId, string securityStamp) =>
            _userManager.Users.Any(x => x.Id == userId && x.SecurityStamp == securityStamp);
        
    }
}
