using FruitChips.Core.Domain.Customers.Entities;

namespace FruitChips.Core.Contracts.Identity.Dtos
{
    public class GenerateTokenDto
    {

      public  Customer User { get; set; }
    }


    public class RsponseTokenInfo
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }    
        public string Token { get; set; }
    }
}
