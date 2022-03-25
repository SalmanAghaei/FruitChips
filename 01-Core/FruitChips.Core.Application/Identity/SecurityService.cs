using FruitChips.Core.Contracts.Identity;
using System.Security.Cryptography;
using System.Text;

namespace FruitChips.Core.Application.Identity
{
    public class SecurityService : ISecurityService
    {

        public string GetSha256Hash(string input)
        {
            using (var hashAlgorithm = new SHA256CryptoServiceProvider())
            {
                if (input is  null )
                    return "";
                var byteValue = Encoding.UTF8.GetBytes(input);
                var byteHash = hashAlgorithm.ComputeHash(byteValue);
                return Convert.ToBase64String(byteHash);
            }
        }
    }
}
