using MongoDB.Driver;
using PaymentService.Domain.Abstractions;
using PaymentService.Infrastructure.Repositories;

namespace PaymentService.API.Extensions
{
    public static class RepositoriesExtension
    {
        public static void AddRepositories(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IReceiptRepository>(sp =>
            {
                var config = sp.GetRequiredService<IConfiguration>();
                var database = sp.GetRequiredService<IMongoDatabase>();
                var collectionName = config["Mongo:ReceiptCollection"];
                return new ReceiptRepository(database, collectionName);
            });
        }
    }
}
