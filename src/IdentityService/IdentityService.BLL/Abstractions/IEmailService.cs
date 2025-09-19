namespace IdentityService.BLL.Abstractions
{
    /// <summary>
    /// Service for sending email messages.
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Sends email message with a token.
        /// </summary>
        /// <param name="email"> Email of user</param>
        /// <param name="token">Token to include in email.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        Task SendMessageAsync(string email, string token, CancellationToken cancellationToken);
    }
}
