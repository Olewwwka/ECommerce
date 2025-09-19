using CatalogService.Application.Handlers;
using CatalogService.Domain.Abstractions.Handlers;
using CatalogService.Domain.Abstractions.Services;
using CatalogService.Infrastructure.Services;

namespace CatalogService.API.Extentions
{
    public static class ServicesConfiguration
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IProductAddedToBasketEventHandler, ProductAddedToBasketEventHandler>();

            services.AddHostedService<ProductAddedToBasketService>();
        }
    }
}
