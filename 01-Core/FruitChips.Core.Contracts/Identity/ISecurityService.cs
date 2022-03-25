using Core.Contracts;

namespace FruitChips.Core.Contracts.Identity
{
    public interface ISecurityService: IScopeLifeTime
    {
        string GetSha256Hash(string input);
    }
}
