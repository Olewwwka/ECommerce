namespace IdentityService.BLL.Abstractions
{
    /// <summary>
    /// Service for password reset operations.
    /// </summary>
    public interface IPasswordResetService
    {
        /// <summary>
        /// Send password reset email message to user.
        /// </summary>
        /// <param name="email">User email address.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        Task SendResetEmailMessageAsync(string email, CancellationToken cancellationToken);

        /// <summary>
        /// Reset suser password using a token.
        /// </summary>
        /// <param name="email">User email address.</param>
        /// <param name="token">Reset token.</param>
        /// <param name="password">New password.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Result of reset password operation</returns>
        Task<bool> ResetPasswordAsync(string email, string token, string password, CancellationToken cancellationToken);
    }
}
