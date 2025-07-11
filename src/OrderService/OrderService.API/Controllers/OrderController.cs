using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderService.Application.DTO;
using OrderService.Application.UseCases.Commands;
using OrderService.Application.UseCases.Queries;
using OrderService.Domain.Enums;

namespace OrderService.API.Controllers
{
    [ApiController]
    [Route("/orders")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{orderId:guid}")]
        public async Task<IActionResult> GetById(Guid orderId, CancellationToken cancellationToken)
        {
            var query = new GetOrderByIdQuery(orderId);

            var result = await _mediator.Send(query, cancellationToken);

            return Ok(result);
        }

        [HttpGet("user/{userId:guid}")]
        public async Task<IActionResult> GetByUserId(Guid userId, CancellationToken cancellationToken)
        {
            var query = new GetOrdersByUserIdQuery(userId);

            var result = await _mediator.Send(query, cancellationToken);

            return Ok(result);
        }

        [HttpPost("{userId:guid}")]
        public async Task<IActionResult> CreateOrder(Guid userId, CreateOrderDto request, CancellationToken cancellationToken)
        {
            var command = new CreateOrderCommand(userId, request);

            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpPut("{orderId:guid}")]
        public async Task<IActionResult> UpdateStatus(Guid orderId, OrderStatus status, CancellationToken cancellationToken)
        {
            var command = new ChangeOrderStatusCommand(orderId, status);

            var result = await _mediator.Send(command, cancellationToken);

            return Ok(result);
        }

        [HttpPut("reject/{orderId:guid}")]
        public async Task<IActionResult> UpdateStatus(Guid orderId, CancellationToken cancellationToken)
        {
            var command = new CancelOrderCommand(orderId);

            var result = await _mediator.Send(command, cancellationToken);

            return Ok(result);
        }
    }
}
