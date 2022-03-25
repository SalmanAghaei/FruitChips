using Microsoft.AspNetCore.Identity;

namespace FruitChips.Core.Domain.Identity.Entities
{
    public class AppToken:IdentityUserToken<Guid>
    {
        public string AccessTokenHash { get; set; }
        public DateTimeOffset AccessTokenExpiresDateTime { get; set; }
    }
    public class AppRole:IdentityRole<Guid>
    {
    }
}
