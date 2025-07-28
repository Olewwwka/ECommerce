using AutoMapper;
using MediatR;
using OrderService.Application.DTO;
using OrderService.Application.UseCases.Commands;
using OrderService.Domain.Abstractions;
using OrderService.Domain.Entities;
using OrderService.Domain.Enums;
using OrderService.Domain.Exceptions;

namespace OrderService.Application.UseCases.CommandHandlers
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
    {
        private readonly IMapper _mapper;
        private readonly IOrderReposotory _orderRepository;
        public CreateOrderCommandHandler(IMapper mapper, IOrderReposotory orderRepository)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
        }
        public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var existingOrders = await _orderRepository.GetByUserId(request.UserId);

            if(existingOrders.Any(o => o.Status == OrderStatus.Pending))
            {
                throw new UserHasUnpaidOrderException($"User with id {request.UserId} have unpaid order!");
            }

            var order = _mapper.Map<Order>(request);

            order.TotalPrice = order.Items.Sum(x => x.Price * x.Count);

            var savedOrder = await _orderRepository.CreateAsync(order);

            return savedOrder;
        }
    }
}
