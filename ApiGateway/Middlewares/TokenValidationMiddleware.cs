using System.Security.Claims;
using GRPC;

namespace ApiGateway.Middleware
{
    public class TokenValidationMiddleware
    {
        private readonly string authPath = "/auth/";
        private readonly RequestDelegate _next;
        private readonly TokenValidator.TokenValidatorClient _client;
        public TokenValidationMiddleware(RequestDelegate next, TokenValidator.TokenValidatorClient client)
        {
            _next = next;
            _client = client;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            var accessToken = context.Request.Cookies["access-token"];

            var path= context.Request.Path.Value;

            if (IsPublicPath(path))
            {
                await _next.Invoke(context);
                return;
            }

            if(accessToken is null)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Access token required");
                return;
            }

            var grpcRequest = new ValidateTokenRequest() { AccessToken = accessToken };
            var grpcResponse = await _client.ValidateTokenAsync(grpcRequest);

            if(!grpcResponse.IsValid)
            {
                context.Response.StatusCode = 401;
                return;
            }

            var claims = grpcResponse.Claims
               .Select(c => new System.Security.Claims.Claim(c.Type, c.Value))
               .ToList();

            var identity = new ClaimsIdentity(claims, "Identity");
            var principal = new ClaimsPrincipal(identity);

            context.User = principal;

            var claimsJson = System.Text.Json.JsonSerializer.Serialize(
                claims.Select(c => new { c.Type, c.Value }));

            context.Request.Headers["X-User-Claims"] = claimsJson;

            await _next.Invoke(context);
        }
        private bool IsPublicPath(string? path)
        {
            return path?.StartsWith(authPath) == true;
        }

    }
}
