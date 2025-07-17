using StackExchange.Redis;

namespace BasketService.API.Extensions
{
    public static class RedisConnection
    {
        public static void ConnectToDatabase(this WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("Redis");

            builder.Services.AddSingleton<IConnectionMultiplexer>(options =>
                 ConnectionMultiplexer.Connect(connectionString));
        }
    }
}
