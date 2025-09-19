using AutoMapper;
using BasketService.Application.DTO;
using BasketService.Application.UseCases.Commands.Baskets;
using BasketService.Domain.Abstractions;
using BasketService.Domain.Entities;
using MediatR;

namespace BasketService.Application.UseCases.CommandHandlers.Baskets
{
    /// <summary>
    /// Handle recieved UpdateBasketCommand.
    /// Creates a new basket if not exists.
    /// </summary>
    public class UpdateBasketCommandHandler : IRequestHandler<UpdateBasketCommand, BasketDto>
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;
        public UpdateBasketCommandHandler(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }
        /// <summary>
        /// Update user basket.
        /// Creates a basket if none exists.
        /// </summary>
        /// <param name="request">Command with the user ID and updated basket.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Updated basket.</returns>
        public async Task<BasketDto> Handle(UpdateBasketCommand request, CancellationToken cancellationToken)
        {
            var existingBasket = await _basketRepository.GetByUserIdAsync(request.userId);

            if (existingBasket is null)
            {
                var basket = new Basket()
                {
                    UserId = request.userId,
                    Items = new List<BasketItem>()
                };

                await _basketRepository.UpdateAsync(basket);
            }

            var basketToUpdate =  _mapper.Map<Basket>(request.basket);

            var updatedBasket = await _basketRepository.UpdateAsync(basketToUpdate);

            var updatedBasketDto = _mapper.Map<BasketDto>(updatedBasket);

            return updatedBasketDto;
        }
    }
}
