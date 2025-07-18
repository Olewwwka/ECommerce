using MongoDB.Driver;

namespace PaymentService.API.Extensions
{
    public static class DatabaseConnection
    {
        public static void ConnectToDb(this WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<IMongoClient>(sp =>
            {
                var config = sp.GetRequiredService<IConfiguration>();
                var connectionString = config["Mongo:ConnectionString"];
                return new MongoClient(connectionString);
            });

            builder.Services.AddScoped<IMongoDatabase>(sp =>
            {
                var config = sp.GetRequiredService<IConfiguration>();
                var client = sp.GetRequiredService<IMongoClient>();
                var dbName = config["Mongo:Database"];
                return client.GetDatabase(dbName);
            });

        }
    }
}
