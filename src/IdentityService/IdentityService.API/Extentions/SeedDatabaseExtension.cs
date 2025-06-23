using IdentityService.DAL.Data;

namespace IdentityService.API.Extentions
{
    public static class SeedDatabaseExtension
    {
        public async static void SeedDatabase(this WebApplication app)
        {
            await DatabaseInitializer.InitializeRolesAsync(app.Services);
        }
    }
}
