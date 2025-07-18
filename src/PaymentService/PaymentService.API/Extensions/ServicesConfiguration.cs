using PaymentService.Application.Abstractions;
using PaymentService.Application.Abstrations;
using PaymentService.Application.Services;
using PaymentService.Domain.Abstractions.Services;
using PaymentService.Infrastructure.Data;

namespace PaymentService.API.Extensions
{
    public static class ServicesConfiguration
    {
        public static void ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IMongoInitializer, MongoInitializer>();
            builder.Services.AddScoped<IPayService, PayService>();
            builder.Services.AddScoped<IReceiptService, ReceiptService>();
        }
    }
}
