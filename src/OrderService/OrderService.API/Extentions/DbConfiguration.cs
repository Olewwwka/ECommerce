using Microsoft.Extensions.Options;
using Npgsql;
using OrderService.Domain.Options;
using System.Data;

namespace OrderService.API.Extensions
{
    public static class DbConfiguration
    {
        public static void AddDb(this WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<IDbConnection>(sp =>
            {
                var options = sp.GetRequiredService<IOptions<DbOptions>>().Value;
                return new NpgsqlConnection(options.OrderService);
            });

        }
    }
}
