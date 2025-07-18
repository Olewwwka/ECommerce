using CatalogService.API.Middlewares;

namespace CatalogService.API.Extentions
{
    public static class MiddlewareExtension
    {
        public static void AddMiddlewares(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
