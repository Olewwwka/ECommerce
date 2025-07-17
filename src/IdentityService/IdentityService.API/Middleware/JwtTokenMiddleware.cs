using IdentityService.BLL.Abstractions;
using IdentityService.BLL.Exceptions;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text.Json;

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

                try
                {
                    var validationResult = accessTokenService.ValidateAccessToken(accessToken);

                    if(validationResult is null)
                    {
                        throw new InvalidAccessTokenException("Invalid token");
                    }
                }
                catch (SecurityTokenExpiredException ex)
                {
                    throw new InvalidAccessTokenException("Token expired: " + ex.Message);
                }
                catch (SecurityTokenValidationException ex)
                {
                    throw new InvalidAccessTokenException("Token validation failed: " + ex.Message);
                }
                catch (SecurityTokenException ex)
                {
                    throw new InvalidAccessTokenException("Invalid token: " + ex.Message);
                }
                catch (Exception ex)
                {
                    throw new InvalidAccessTokenException("Unknown token validation error: " + ex.Message);
                }
            }

            await _next(context);
        }
    }
}
