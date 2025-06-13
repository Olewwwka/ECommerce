using FluentEmail.Core;
using IdentityService.BLL.Abstractions;
using IdentityService.BLL.Constants;

namespace IdentityService.BLL.Services
{
    public class EmailService : IEmailService
    {
        private readonly IFluentEmail _fluentEmail;
        public EmailService(IFluentEmail fluentEmail)
        {
            _fluentEmail = fluentEmail;
        }

        public async Task SendMessageAsync(string email, string token, CancellationToken cancellationToken)
        {
            await _fluentEmail
                .To(email)
                .Subject(EmailConstants.Subject)
                .Body(EmailConstants.Body + "\n-------------------\n" + token + "\n-------------------\n")
                .SendAsync(cancellationToken);
        }
    }
}
