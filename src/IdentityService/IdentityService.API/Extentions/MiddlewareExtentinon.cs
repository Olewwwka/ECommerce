using IdentityService.API.Middleware;

namespace IdentityService.API.Extentions
{
    public static class MiddlewareExtentinon
    {
        public static void AddMiddlewares(this IApplicationBuilder app)
        {
            app.UseMiddleware<JwtTokenMiddleware>();
            app.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}
