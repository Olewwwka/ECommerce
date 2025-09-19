using IdentityService.BLL.DTO;

namespace IdentityService.BLL.Abstractions
{
    /// <summary>
    /// Service for user authentication operations.
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Register a new user
        /// </summary>
        /// <param name="request">Information about user.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>AuthResponse what contains pair of access and refresh tokens.</returns>
        Task<AuthResponse> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken);
        /// <summary>
        /// Login in a user and returns authentication response.
        /// </summary>
        /// <param name="request">Information about user.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>AuthResponse what contains pair of access and refresh tokens.</returns>
        Task<AuthResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken);
        /// <summary>
        /// Refresh access and refresh tokens.
        /// </summary>
        /// <param name="accessToken">Access token.</param>
        /// <param name="refreshToken">Refresh token.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>New AuthResponse with refreshed tokens.</returns>
        Task<AuthResponse> RefreshAsync(string accessToken, string refreshToken, CancellationToken cancellationToken);
    }
}
