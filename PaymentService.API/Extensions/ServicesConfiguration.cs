using PaymentService.Application.Abstractions;
using PaymentService.Application.Abstrations;
using PaymentService.Application.Handlers;
using PaymentService.Application.Services;
using PaymentService.Domain.Abstractions.Handlers;
using PaymentService.Domain.Abstractions.Services;
using PaymentService.Infrastructure.Data;
using PaymentService.Infrastructure.Services;

namespace PaymentService.API.Extensions
{
    public static class ServicesConfiguration
    {
        public static void ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IMongoInitializer, MongoInitializer>();
            builder.Services.AddScoped<IPayService, PayService>();
            builder.Services.AddScoped<IReceiptService, ReceiptService>();
            builder.Services.AddScoped<IWithdrawBallanceService, WithdrawBallanceService>();

            builder.Services.AddScoped<IOrderCreatedEventHandler, OrderCreatedEventHandler>();

            builder.Services.AddHttpClient<IWithdrawBallanceService, WithdrawBallanceService>();


            builder.Services.AddHostedService<OrderCreatedService>();
        }
    }
}
