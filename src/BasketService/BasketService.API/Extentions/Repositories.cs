using BasketService.Domain.Abstractions;
using BasketService.Infrastructure.Repositories;

namespace BasketService.API.Extentions
{
    public static class Repositories
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBasketRepository, BasketRepository>();
        }
    }
}
