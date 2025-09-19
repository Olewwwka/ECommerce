using IdentityService.BLL.Abstractions;
using IdentityService.BLL.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IdentityService.BLL.Services
{
    /// <summary>
    /// Serivce that contains logic to working with access tokens of users
    /// Contains methods to generate and validate access tokens of users
    /// </summary>
    public class AccessTokenService : IAccessTokenService
    {
        private readonly JwtOptions _jwtOptions;
        public AccessTokenService(IOptions<JwtOptions> options)
        {
            _jwtOptions = options.Value;
        }
        /// <summary>
        /// Generate access token for user
        /// </summary>
        /// <param name="id">Unique ID of user</param>
        /// <param name="name">Name of user</param>
        /// <param name="email">Email of user</param>
        /// <param name="Roles">List of user roles</param>
        /// <returns>Access token</returns>
        public string GenerateAccessToken(Guid id, string name, string email, IEnumerable<string> Roles)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, id.ToString()),
                new(ClaimTypes.Name, name),
                new(ClaimTypes.Email, email),
            };

            claims.AddRange(Roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));

            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var accessToken = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(_jwtOptions.ExpiryHours),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(accessToken);
        }
        /// <summary>
        /// Validationg existing access token
        /// </summary>
        /// <param name="accessToken">Access token of user</param>
        /// <returns>List of claims</returns>
        public ClaimsPrincipal ValidateAccessToken(string accessToken)
        {
            var accessTokenHandler = new JwtSecurityTokenHandler();

            var accesTokenValidationParametrs = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidIssuer = _jwtOptions.Issuer,

                ValidateAudience = true,
                ValidAudience = _jwtOptions.Audience,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)),

                ClockSkew = TimeSpan.Zero,
                ValidateLifetime = true,
            };
            var claims = accessTokenHandler.ValidateToken(accessToken, accesTokenValidationParametrs, out SecurityToken validatedToken);

            return claims;
        }
    }
}
