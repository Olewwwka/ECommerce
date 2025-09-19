using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Encodings.Web;

namespace OrderService.API.Handlers
{
    public class AuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public AuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder)
            : base(options, logger, encoder) { }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.TryGetValue("X-User-Claims", out var claimsJson))
                return Task.FromResult(AuthenticateResult.Fail("No claims header"));

            List<ClaimData>? claimsData;
            try
            {
                claimsData = JsonSerializer.Deserialize<List<ClaimData>>(claimsJson);
            }
            catch
            {
                return Task.FromResult(AuthenticateResult.Fail("Invalid claims header"));
            }

            if (claimsData == null)
                return Task.FromResult(AuthenticateResult.Fail("Empty claims"));

            var claims = new List<Claim>();
            foreach (var c in claimsData)
            {
                claims.Add(new Claim(c.Type, c.Value));
            }

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return Task.FromResult(AuthenticateResult.Success(ticket));
        }

        private class ClaimData
        {
            public string Type { get; set; } = string.Empty;
            public string Value { get; set; } = string.Empty;
        }
    }

}