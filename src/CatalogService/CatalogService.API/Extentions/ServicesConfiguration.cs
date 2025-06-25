using CatalogService.Domain.Abstractions.Services;
using CatalogService.Domain.Options;
using CatalogService.Infrastructure.Services;

namespace CatalogService.API.Extentions
{
    public static class ServicesConfiguration
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IFileService, FileService>();
        }
    }
}
