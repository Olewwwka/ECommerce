using Ecommerce.Brokers.Events;
using IdentityService.BLL.Abstractions;
using IdentityService.BLL.Exceptions;
using IdentityService.DAL.Abstractions;
using IdentityService.DAL.Abstractions.Handlers;
using Microsoft.Extensions.Logging;

namespace IdentityService.BLL.EventHandlers
{
    public class PaymentFailedEventHandler : IPaymentFailedEventHandler
    {
        private readonly IEmailService _emailService;
        private readonly IUsersRepository _usersRepository;
        private readonly ILogger<PaymentFailedEventHandler> _logger;
        public PaymentFailedEventHandler(IEmailService emailService, IUsersRepository usersRepository, ILogger<PaymentFailedEventHandler> logger)
        {
            _emailService = emailService;
            _usersRepository = usersRepository;
            _logger = logger;
        }
        public async Task HandleAsync(PaymentFailedEvent paymentFailedEvent, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling PaymentFailedEvent for User ID: {UserId}, Order ID: {OrderId}", paymentFailedEvent.UserId, paymentFailedEvent.OrderId);

            var existingUser = await _usersRepository.GetByIdAsync(paymentFailedEvent.UserId, cancellationToken);

            Console.WriteLine(existingUser.Email);
            if(existingUser is null)
            {
                throw new UserNotFoundException($"User with ID {paymentFailedEvent.UserId} not found");
            }

            var message = $"Your order with ID {paymentFailedEvent.OrderId} has been cancelled. Reason: {paymentFailedEvent.Message}.";

            Console.WriteLine(message);

            await _emailService.SendMessageAsync(existingUser.Email, message, cancellationToken);

            _logger.LogInformation("Sent cancellation email to User ID: {UserId} for Order ID: {OrderId}", paymentFailedEvent.UserId, paymentFailedEvent.OrderId);
        }
    }
}
