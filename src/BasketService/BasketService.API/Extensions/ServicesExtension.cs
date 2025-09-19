using BasketService.Application.Handlers;
using BasketService.Domain.Abstractions.Handlers;
using BasketService.Infrastructure.Services;

namespace BasketService.API.Extensions
{
    public static class ServicesExtension
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IOrderComplitedEventHandler, OrderCreatedEventHandler>();
            services.AddScoped<IProductDeletedEventHandler, ProductDeletedEventHandler>();  

            services.AddHostedService<ProductDeletedService>();
            services.AddHostedService<OrderComplitedService>();
        }
    }
}
