using BasketService.API.Middleware;

namespace BasketService.API.Extensions
{
    public static class MiddlewareExtension
    {
        public static void AddMiddlewares(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
