using OrderService.Domain.Abstractions.Services;
using OrderService.Infrastructure.Services;

namespace OrderService.API.Extentions
{
    public static class ServicesDI
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddSingleton<IDbInitializer, DbInitializer>();
        }
    }
}
