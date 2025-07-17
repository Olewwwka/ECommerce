using System;
using System.Security.Claims;

namespace IdentityService.BLL.Abstractions
{
    public interface IAccessTokenService
    {
        string GenerateAccessToken(Guid id, string name, string email, IEnumerable<string> roles);
        ClaimsPrincipal ValidateAccessToken(string accessToken);
    }
}
