using IdentityService.DAL.Abstractions.Services;
using IdentityService.DAL.Data;
using Microsoft.AspNetCore.Http.Extensions;

namespace IdentityService.API.Extentions
{
    public static class SeedDatabaseExtension
    {
        public async static void SeedDatabase(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var initializer = scope.ServiceProvider.GetRequiredService<IDatabaseInitializer>();

            await initializer.InitializeRolesAsync(app.Services);
        }
    }
}
