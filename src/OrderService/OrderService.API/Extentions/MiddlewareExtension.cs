using OrderService.API.Middlewares;

namespace OrderService.API.Extentions
{
    public static class MiddlewareExtension
    {
        public static void AddMiddlewares(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
