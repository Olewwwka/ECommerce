using BasketService.Application.UseCases.Commands.Baskets;
using BasketService.Domain.Abstractions;
using BasketService.Domain.Exceptions;
using MediatR;

namespace BasketService.Application.UseCases.CommandHandlers.Baskets
{
    public class DeleteBasketCommandHandler : IRequestHandler<DeleteBasketCommand, Guid>
    {
        private readonly IBasketRepository _basketRepository;
        public DeleteBasketCommandHandler(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

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
