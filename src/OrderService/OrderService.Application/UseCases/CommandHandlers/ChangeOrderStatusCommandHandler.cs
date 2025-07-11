using AutoMapper;
using MediatR;
using OrderService.Application.DTO;
using OrderService.Application.UseCases.Commands;
using OrderService.Domain.Abstractions;
using OrderService.Domain.Enums;
using OrderService.Domain.Exceptions;

namespace OrderService.Application.UseCases.CommandHandlers
{
    public class ChangeOrderStatusCommandHandler : IRequestHandler<ChangeOrderStatusCommand, Guid>
    {
        private readonly IMapper _mapper;
        private readonly IOrderReposotory _orderRepository;
        public ChangeOrderStatusCommandHandler(IMapper mapper, IOrderReposotory orderRepository)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
        }
        public async Task<Guid> Handle(ChangeOrderStatusCommand request, CancellationToken cancellationToken)
        {
            var existingOrder = await _orderRepository.GetByIdAsync(request.orderId);

            if (existingOrder is null)
            {
                throw new NotFoundException($"Order with id {request.orderId} not found!");
            }

            if(!Enum.IsDefined(typeof(OrderStatus), request.newStatus))
            {
                throw new NotFoundException($"Status {request.newStatus} not found!");
            }

            var guid = await _orderRepository.UpdateStatusAsync(request.orderId, request.newStatus);

            return guid;
        }
    }
}
