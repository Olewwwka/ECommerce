using IdentityService.BLL.Abstractions;

namespace IdentityService.API.Middleware
{
    public class JwtTokenMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtTokenMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var accessToken = context.Request.Cookies["access-token"];  

            if (!string.IsNullOrEmpty(accessToken))
            {
                var accessTokenService = context.RequestServices.GetRequiredService<IAccessTokenService>();

                var validationResult = accessTokenService.ValidateAccessToken(accessToken);

                if (!validationResult.IsValid)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return;
                }

                context.User = validationResult.Claims;
            }
            await _next(context);
        }
    }
}
