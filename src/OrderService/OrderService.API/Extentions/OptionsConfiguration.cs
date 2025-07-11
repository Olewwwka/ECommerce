using OrderService.Domain.Options;

namespace OrderService.API.Extentions
{
    public static class OptionsConfiguration
    {
        public static void ConfigureOptions(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<DbOptions>(builder.Configuration.GetSection("ConnectionStrings"));
        }
    }
}
