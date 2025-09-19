using BasketService.Application.UseCases.Commands.Baskets;
using BasketService.Domain.Abstractions;
using BasketService.Domain.Entities;
using Ecommerce.Brokers.Abstractions;
using Ecommerce.Brokers.Events;
using MediatR;

namespace BasketService.Application.UseCases.CommandHandlers.Baskets
{
    /// <summary>
    /// Handle recieved AddItemToBasketCommand.
    /// </summary>
    public class AddItemToBasketCommandHandler : IRequestHandler<AddItemToBasketCommand, Guid>
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IEventProducer _producer;
        public AddItemToBasketCommandHandler(IBasketRepository basketRepository, IEventProducer producer)
        {
            _basketRepository = basketRepository;
            _producer = producer;
        }
        /// <summary>
        /// Add item to user basket.
        /// </summary>
        /// <param name="request">Command with user ID, product ID, and quantity.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>User ID of updated basket.</returns>
        public async Task<Guid> Handle(AddItemToBasketCommand request, CancellationToken cancellationToken)
        {
            var existingBasket = await _basketRepository.GetByUserIdAsync(request.UserId);
            if (existingBasket is null)
            {
                existingBasket = new Basket { UserId = request.UserId };
            }
            var item = new BasketItem
            {
                ProductId = request.ProductId,
                Quantity = request.Quantity
            };
            existingBasket.AddItem(item);

            await _basketRepository.UpdateAsync(existingBasket);

            var basketEvent = new ProductAddedToBasketEvent(request.UserId, request.ProductId, request.Quantity);

            await _producer.SendAsync(basketEvent, cancellationToken);

            return existingBasket.UserId;
        }
    }
}
