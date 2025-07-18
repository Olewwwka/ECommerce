using PaymentService.Domain.Options;

namespace PaymentService.API.Extensions
{
    public static class OptionsConfiguration
    {
        public static void ConfigureOptions(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<MongoOptions>(
                builder.Configuration.GetSection("Mongo"));
        }
    }
}
