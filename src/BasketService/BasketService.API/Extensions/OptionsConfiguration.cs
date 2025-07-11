using BasketService.Domain.Options;

namespace BasketService.API.Extensions
{
    public static class OptionsConfiguration
    {
        public static void ConfigureOptions(this WebApplicationBuilder builder)
        {
            var config = builder.Configuration.GetSection(nameof(RedisOptions));

            builder.Services.Configure<RedisOptions>(config);
        }
    }
}
