using AutoMapper;
using BasketService.Application.DTO;
using BasketService.Application.UseCases.Queries.Baskets;
using BasketService.Domain.Abstractions;
using BasketService.Domain.Entities;
using BasketService.Domain.Exceptions;
using MediatR;

namespace BasketService.Application.UseCases.QueryHandlers
{
    public class GetBasketByUserIdQueryHandler : IRequestHandler<GetBasketByUserIdQuery, BasketDto>
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;
        public GetBasketByUserIdQueryHandler(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }
        public async Task<BasketDto> Handle(GetBasketByUserIdQuery request, CancellationToken cancellationToken)
        {
            var existingBasket = await _basketRepository.GetByUserIdAsync(request.userId);

            if (existingBasket is null)
            {
                var newBasket = new Basket()
                {
                    UserId = request.userId,
                    Items = new List<BasketItem>()
                };

                await _basketRepository.UpdateAsync(newBasket);

                var basket = _mapper.Map<BasketDto>(newBasket);

                return basket;
            }

            var basketDto = _mapper.Map<BasketDto>(existingBasket);

            return basketDto;
        }
    }
}
