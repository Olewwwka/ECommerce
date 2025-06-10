using IdentityService.API.Middleware;

namespace IdentityService.API.Extentions
{
    public static class MiddlewareExtentinon
    {
        public static void AddMiddlewares(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<JwtTokenMiddleware>();
            builder.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}
