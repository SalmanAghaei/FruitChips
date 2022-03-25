using FruitChips.Core.Domain.Customers.Entities;
using FruitChips.Core.Domain.Identity.Entities;
using Microsoft.AspNetCore.Identity;

namespace FruitChips.Persistance.Identity.SqlData
{
    public static class ApplicationDbInitializer
    {
        public static void SeedAdminUser(
            UserManager<Customer> userManager,
            RoleManager<AppRole> roleManeger
            )
        {
          
            Guid userId = Guid.NewGuid();
            Customer user = new Customer
            {
                Id = userId,
                FirstName="admin",
                LastName="admin",
                UserName = "Admin@gmail.com",
                Email = "Admin@gmail.com",
                IsSystemAdmin = true
            };

            if (userManager.FindByEmailAsync("Admin@gmail.com").Result == null)
            {


                IdentityResult result = userManager.CreateAsync(user, "Fruit123$").Result;

            }


        }
    }
}
