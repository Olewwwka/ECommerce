using AutoMapper;
using MediatR;
using OrderService.Application.DTO;
using OrderService.Application.UseCases.Queries;
using OrderService.Domain.Abstractions;
using OrderService.Domain.Exceptions;

namespace OrderService.Application.UseCases.QueryHandlers
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderDto>
    {
        private readonly IMapper _mapper;
        private readonly IOrderReposotory _orderRepository;
        public GetOrderByIdQueryHandler(IMapper mapper, IOrderReposotory orderReposotory)
        {
            _mapper = mapper;
            _orderRepository = orderReposotory;
        }
        public async Task<OrderDto> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var exisingOrder = await _orderRepository.GetByIdAsync(request.orderId);

            if(exisingOrder is null)
            {
                throw new NotFoundException($"Order with id {request.orderId} not found!");
            }

            var order = _mapper.Map<OrderDto>(exisingOrder);

            return order;
        }
    }
}
