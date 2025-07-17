using IdentityService.API.Middleware;

namespace IdentityService.API.Extentions
{
    public static class MiddlewareExtension
    {
        public static void AddMiddlewares(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseMiddleware<JwtTokenMiddleware>();
        }
    }
}
