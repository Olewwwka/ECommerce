using FluentEmail.Core;
using IdentityService.BLL.Abstractions;
using IdentityService.BLL.Constants;
using Microsoft.Extensions.Logging;

namespace IdentityService.BLL.Services
{
    /// <summary>
    /// Serivce that contains logic to send email message to user
    /// </summary>
    public class EmailService : IEmailService
    {
        private readonly IFluentEmail _fluentEmail;
        private readonly ILogger<EmailService> _logger;
        public EmailService(IFluentEmail fluentEmail, ILogger<EmailService> logger)
        {
            _fluentEmail = fluentEmail;
            _logger = logger;
        }
        /// <summary>
        /// Send email notification to user email
        /// </summary>
        /// <param name="email">Name of user</param>
        /// <param name="message">Message to send</param>
        /// <param name="cancellationToken">A token that allows you to cancel an asynchronous operation</param>
        public async Task SendMessageAsync(string email, string message, CancellationToken cancellationToken)
        {
            await _fluentEmail
                .To(email)
                .Subject(EmailConstants.Subject)
                .Body("\n-------------------\n" + message + "\n-------------------\n")
                .SendAsync(cancellationToken);
            _logger.LogInformation("Email sent to {Email} with message: {Message}", email, message);
        }
    }
}
