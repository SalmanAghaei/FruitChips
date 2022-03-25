using FruitChips.Core.Domain.Customers.Entities;
using FruitChips.Core.Domain.Identity.Entities;
using FruitChips.Persistance.Identity.SqlData.Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FruitChips.Persistance.Identity.SqlData
{
    public static class ServiceExtentions
    {
        public static IServiceCollection AddIdentityDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<Customer, AppRole>()
               .AddEntityFrameworkStores<IdentityContext>()
               .AddDefaultTokenProviders();
            //Register Dbcontext
            services.AddDbContext<IdentityContext>(options => {
                //    options.UseInMemoryDatabase("DataBase");

                options.UseSqlServer(configuration.GetConnectionString("cnn"),
                    sqlServerOptionsAction: sqlOptions =>
                    {
                        sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                    });
                });
            return services;
        }
    }
}
