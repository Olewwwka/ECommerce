using CatalogService.Domain.Options;

namespace CatalogService.API.Extentions
{
    public static class OptionsConfigutation
    {
        public static void ConfigureOptions(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<FilesOptions>(builder.Configuration.GetSection(nameof(FilesOptions)));
        }
    }
}
