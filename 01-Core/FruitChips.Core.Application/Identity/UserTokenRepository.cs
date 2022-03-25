using FruitChips.Core.Contracts.Identity;
using FruitChips.Core.Domain.Identity.Entities;
using System.Linq.Expressions;
using FruitChips.Persistance.Identity.SqlData.Context;
using Microsoft.EntityFrameworkCore;

namespace FruitChips.Core.Application.Identity
{
    public class UserTokenRepository : IUserTokenRepository
    {
        private readonly IdentityContext _identityContext;

        public UserTokenRepository(IdentityContext identityContext)
        {
            _identityContext = identityContext;
        }
        public void SaveToken(AppToken userToken)
        {
            _identityContext.UserTokens.Add(userToken);
        }


        public void SaveChanges()
        {
            _identityContext.SaveChanges();
        }

        public void RemoveToken(Guid userId)
        {
            var usertokens = _identityContext.UserTokens.Where(x => x.UserId == userId);
            _identityContext.UserTokens.RemoveRange(usertokens);
            SaveChanges();
        }
        public AppToken Get(Expression<Func<AppToken, bool>> expression)
        {
            return _identityContext.UserTokens.Where(expression).AsNoTracking().FirstOrDefault();
        }


        public bool ExistToken(string tokenHash)=>
            _identityContext.UserTokens.Any(x => x.AccessTokenHash == tokenHash);


    }
}
