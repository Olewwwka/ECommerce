using Ecommerce.Brokers.Events;
using IdentityService.BLL.Abstractions;
using IdentityService.BLL.Exceptions;
using IdentityService.DAL.Abstractions;
using IdentityService.DAL.Abstractions.Handlers;
using Microsoft.Extensions.Logging;

namespace IdentityService.BLL.EventHandlers
{
    public class OrderComlitedEventHandler : IOrderComlitedEventHandler
    {
        private readonly IEmailService _emailService;
        private readonly IUsersRepository _usersRepository;
        private readonly ILogger<OrderComlitedEventHandler> _logger;
        public OrderComlitedEventHandler(IEmailService emailService, IUsersRepository usersRepository, ILogger<OrderComlitedEventHandler> logger)
        {
            _emailService = emailService;
            _usersRepository = usersRepository;
            _logger = logger;
        }
        public async Task HandleAsync(OrderCompletedEvent orderCompletedEvent, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling OrderCompletedEvent for Order ID: {OrderId}", orderCompletedEvent.OrderId);

            var existingUser = await _usersRepository.GetByIdAsync(orderCompletedEvent.UserId, cancellationToken);
            Console.WriteLine(existingUser.Email);
            if (existingUser is null)
            {
                throw new UserNotFoundException($"User with ID {orderCompletedEvent.UserId} not found");
            }

            var message = $"Your order with ID {orderCompletedEvent.OrderId} was succesfully complited! {orderCompletedEvent.CompletedAt}.";
            Console.WriteLine(message);
            await _emailService.SendMessageAsync(existingUser.Email, message, cancellationToken);

            _logger.LogInformation("Sent success email to User ID: {UserId} for Order ID: {OrderId}", orderCompletedEvent.UserId, orderCompletedEvent.OrderId);
        }
    }
}
