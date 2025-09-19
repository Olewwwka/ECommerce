using IdentityService.DAL.Entities;

namespace IdentityService.BLL.DTO
{
    /// <summary>
    /// Response object returned after authentication.
    /// Contains access token and refresh token.
    /// </summary>
    public class AuthResponse
    {
        public string AccessToken { get; set; }
        public RefreshToken RefreshToken { get; set; }
    }
}
