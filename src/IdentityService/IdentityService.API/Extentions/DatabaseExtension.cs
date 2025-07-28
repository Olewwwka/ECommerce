using IdentityService.DAL.Data;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System.Diagnostics.Contracts;

namespace IdentityService.API.Extentions
{
    public static class DatabaseExtension
    {
        public static void AddDatabases(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<IdentityServiceDbContext>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(IdentityServiceDbContext)));
            });

            builder.Services.AddSingleton<IConnectionMultiplexer>(cm =>
                ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("Redis")));
        }
    }
}
