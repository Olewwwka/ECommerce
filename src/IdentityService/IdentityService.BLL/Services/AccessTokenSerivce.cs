using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using IdentityService.BLL.Abstractions;
using IdentityService.BLL.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using IdentityService.BLL.DTO;

namespace IdentityService.BLL.Services
{
    public class AccessTokenSerivce : IAccessTokenService
    {
        private readonly JwtOptions _jwtOptions;
        public AccessTokenSerivce(IOptions<JwtOptions> options)
        {
            _jwtOptions = options.Value;
        }

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

        public DTO.TokenValidationResult ValidateAccessToken(string accessToken)
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

            try
            {
                var claims = accessTokenHandler.ValidateToken(accessToken, accesTokenValidationParametrs, out SecurityToken validatedToken);

                return new DTO.TokenValidationResult()
                {
                    IsValid = true,
                    Claims = claims
                };
            }
            catch (SecurityTokenExpiredException ex)
            {
                return new DTO.TokenValidationResult()
                {
                    IsValid = false,
                    ErrorMessage = "This token expired" + ex.Message
                };
            }
            catch (SecurityTokenArgumentException ex)
            {
                return new DTO.TokenValidationResult()
                {
                    IsValid = false,
                    ErrorMessage = "Access token has invalid arguments" + ex.Message
                };
            }
            catch (SecurityTokenValidationException ex)
            {
                return new DTO.TokenValidationResult()
                {
                    IsValid = false,
                    ErrorMessage = "Error validating token" + ex.Message
                };
            }
            catch (SecurityTokenException ex)
            {
                return new DTO.TokenValidationResult()
                {
                    IsValid = false,
                    ErrorMessage = "Invalid token" + ex.Message
                };
            }
            catch (Exception ex)
            {
                return new DTO.TokenValidationResult()
                {
                    IsValid = false,
                    ErrorMessage = "Error" + ex.Message
                };
            }
        }
    }
}
