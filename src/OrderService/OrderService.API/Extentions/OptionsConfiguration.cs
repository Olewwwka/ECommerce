using OrderService.Domain.Options;

namespace OrderService.API.Extensions
{
    public static class OptionsConfiguration
    {
        public static void ConfigureOptions(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<DbOptions>(builder.Configuration.GetSection("ConnectionStrings"));
        }
    }
}
