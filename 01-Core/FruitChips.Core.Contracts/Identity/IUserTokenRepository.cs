using Core.Contracts;
using FruitChips.Core.Domain.Identity.Entities;
using System.Linq.Expressions;

namespace FruitChips.Core.Contracts.Identity
{
    public interface IUserTokenRepository: IScopeLifeTime
    {
        bool ExistToken(string tokenHash);
        public void SaveToken(AppToken userToken);
        AppToken Get(Expression<Func<AppToken, bool>> expression);
        void RemoveToken(Guid guid);
        void SaveChanges();
    }
}
