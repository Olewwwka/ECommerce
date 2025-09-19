using AutoMapper;
using MediatR;
using OrderService.Application.DTO;
using OrderService.Application.UseCases.Queries;
using OrderService.Domain.Abstractions;

namespace OrderService.Application.UseCases.QueryHandlers
{
    /// <summary>
    /// Handler for GetOrdersByUserIdQuery.
    /// </summary>
    public class GetOrdersByUserIdQueryHandler : IRequestHandler<GetOrdersByUserIdQuery, List<OrderDto>>
    {
        private readonly IMapper _mapper;
        private readonly IOrderReposotory _orderRepository;
        public GetOrdersByUserIdQueryHandler(IMapper mapper, IOrderReposotory orderReposotory)
        {
            _mapper = mapper;
            _orderRepository = orderReposotory;
        }
        /// <summary>
        /// Handles the GetOrdersByUserIdQuery/>.
        /// </summary>
        /// <param name="request">Query with user ID.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A list of OrderDto.</returns>
        public async Task<List<OrderDto>> Handle(GetOrdersByUserIdQuery request, CancellationToken cancellationToken)
        {
            var exisingOrders = await _orderRepository.GetByUserId(request.userId);

            var orders = _mapper.Map<List<OrderDto>>(exisingOrders);

            return orders;
        }
    }
}
