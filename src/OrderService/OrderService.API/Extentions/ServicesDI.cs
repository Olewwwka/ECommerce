using OrderService.Application.Handlers;
using OrderService.Domain.Abstractions.Handlers;
using OrderService.Domain.Abstractions.Services;
using OrderService.Infrastructure.Services;

namespace OrderService.API.Extensions
{
    public static class ServicesDI
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IPaymentFailedEventHandler, PaymentFailedEventHandler>();
            services.AddScoped<IPaymentSucceedEventHandler, PaymentSucceedEventHandler>();


            services.AddHostedService<PaymentFailedService>();
            services.AddHostedService<PaymentSucceedService>();

            services.AddSingleton<IDbInitializer, DbInitializer>();
        }
    }
}
