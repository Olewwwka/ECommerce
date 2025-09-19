using System.Security.Claims;

namespace IdentityService.BLL.Abstractions
{
    /// <summary>
    /// Service for generating and validating access tokens.
    /// </summary>
    public interface IAccessTokenService
    {
        /// <summary>
        /// Generates access token for user.
        /// </summary>
        /// <param name="id">User ID.</param>
        /// <param name="name">User name</param>
        /// <param name="email">User email</param>
        /// <param name="roles">User roles</param>
        /// <returns>Access token</returns>
        string GenerateAccessToken(Guid id, string name, string email, IEnumerable<string> roles);
        /// <summary>
        /// Validate access token.
        /// </summary>
        /// <param name="accessToken">Access token to validate.</param>
        /// <returns>Claims about user</returns>
        ClaimsPrincipal ValidateAccessToken(string accessToken);
    }
}
