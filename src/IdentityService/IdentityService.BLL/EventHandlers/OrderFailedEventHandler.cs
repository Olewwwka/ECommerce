using Ecommerce.Brokers.Events;
using IdentityService.BLL.Abstractions;
using IdentityService.BLL.Exceptions;
using IdentityService.DAL.Abstractions;
using IdentityService.DAL.Abstractions.Handlers;
using Microsoft.Extensions.Logging;

namespace IdentityService.BLL.EventHandlers
{
    public class OrderFailedEventHandler : IOrderFailedEventHandler
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IHttpService _httpService;
        private readonly ILogger<OrderFailedEventHandler> _logger;
        private readonly IEmailService _emailService;
        public OrderFailedEventHandler(IUsersRepository usersRepository, 
            IHttpService httpService, 
            ILogger<OrderFailedEventHandler> logger,
            IEmailService emailService)
        {
            _usersRepository = usersRepository;
            _httpService = httpService;
            _logger = logger;
            _emailService = emailService;
        }
        public async Task HandleAsync(OrderFailedEvent orderFailedEvent, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling OrderFailedEvent for Order ID: {OrderId}", orderFailedEvent.OrderId);

            var response = await _httpService.TryGetReceiptAsync(orderFailedEvent.OrderId, cancellationToken);

            if(response is null)
            {
                throw new Exception($"Failed to retrieve receipt for Order ID: {orderFailedEvent.OrderId}");
            }

            var user = await _usersRepository.GetByIdAsync(orderFailedEvent.UserId, cancellationToken);

            if(user is null)
            {
                throw new UserNotFoundException($"User with ID {orderFailedEvent.UserId} not found");
            }

            user.Ballance += response.TotalCost;

            await _usersRepository.UpdateAsync(user, cancellationToken);

            _logger.LogInformation("User ID: {UserId} balance updated after order failure. New balance: {NewBalance}", user.Id, user.Ballance);

            await _emailService.SendMessageAsync(user.Email, $"Yours balance updated after order failure. New balance: {user.Ballance}", cancellationToken);
        }
    }
}
