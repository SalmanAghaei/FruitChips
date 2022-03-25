using FruitChips.Core.Contracts.Products.Repositories;
using FruitChips.Persistance.SqlData.Products.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace FruitChips.Persistance.SqlData
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddPersistanceServices(this IServiceCollection services)
        {
            return services.AddScoped<IProductCommandRepository, ProductCommandRepository>();
        }
    }
}
