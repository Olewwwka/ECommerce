using BasketService.Domain.Abstractions;
using BasketService.Domain.Abstractions.Handlers;
using Ecommerce.Brokers.Events;

namespace BasketService.Application.Handlers
{
    public class OrderCreatedEventHandler : IOrderComplitedEventHandler
    {
        private readonly IBasketRepository _basketRepository;
        public OrderCreatedEventHandler(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }
        public async Task HandleAsync(OrderCompletedEvent orderCompletedEvent, CancellationToken cancellationToken)
        {

            if (orderCompletedEvent is null)
            {
                throw new ArgumentNullException(nameof(orderCompletedEvent), "OrderCreatedEvent cannot be null");
            }

            var existingbasket = await _basketRepository.GetByUserIdAsync(orderCompletedEvent.UserId);

            if( existingbasket is null)
            {
                throw new Exception($"Basket for user with ID {orderCompletedEvent.UserId} not found");
            }
            
            await _basketRepository.DeleteAsync(orderCompletedEvent.UserId);
        }
    }
}
