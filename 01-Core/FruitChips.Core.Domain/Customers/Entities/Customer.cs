using Microsoft.AspNetCore.Identity;

namespace FruitChips.Core.Domain.Customers.Entities
{
    public class Customer : IdentityUser<Guid>
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsSystemAdmin { get; set; }

    }

}
