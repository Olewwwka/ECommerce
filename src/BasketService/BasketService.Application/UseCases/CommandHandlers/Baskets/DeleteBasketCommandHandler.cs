using BasketService.Application.UseCases.Commands.Baskets;
using BasketService.Domain.Abstractions;
using BasketService.Domain.Exceptions;
using MediatR;

namespace BasketService.Application.UseCases.CommandHandlers.Baskets
{
    /// <summary>
    /// Handle recieved DeleteBasketCommand.
    /// </summary>
    public class DeleteBasketCommandHandler : IRequestHandler<DeleteBasketCommand, Guid>
    {
        private readonly IBasketRepository _basketRepository;
        public DeleteBasketCommandHandler(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }
        /// <summary>
        /// Delete basket for current user ID.
        /// </summary>
        /// <param name="request">Command with the user ID.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>User Id of deleted basket.</returns>
        /// <exception cref="NotFoundException">Throws if basket not exist.</exception>
        public async Task<Guid> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
        {
            var existingBasket = await _basketRepository.GetByUserIdAsync(request.userId);

            if(existingBasket is null)
            {
                throw new NotFoundException($"Basket for user {request.userId} not found!");
            }

            await _basketRepository.DeleteAsync(request.userId);

            return request.userId;
        }
    }
}
