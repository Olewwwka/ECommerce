using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderService.Application.DTO;
using OrderService.Application.UseCases.Commands;
using OrderService.Application.UseCases.Queries;
using OrderService.Domain.Enums;

namespace OrderService.API.Controllers
{
    /// <summary>
    /// Controller for orders.
    /// Provides endpoints to create, retrieve, update, and cancel orders.
    /// </summary>
    [ApiController]
    [Route("/orders")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get order by ID.
        /// </summary>
        /// <param name="orderId">Unique ID of order.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Order details.</returns>
        [HttpGet("{orderId:guid}")]
        public async Task<IActionResult> GetById(Guid orderId, CancellationToken cancellationToken)
        {
            var query = new GetOrderByIdQuery(orderId);
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Get all orders for user.
        /// </summary>
        /// <param name="userId">Unique ID of user.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>List of orders.</returns>
        [HttpGet("user/{userId:guid}")]
        public async Task<IActionResult> GetByUserId(Guid userId, CancellationToken cancellationToken)
        {
            var query = new GetOrdersByUserIdQuery(userId);
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Create new order for user.
        /// </summary>
        /// <param name="userId">Unique ID of user.</param>
        /// <param name="request">Order data.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>created order.</returns>
        [HttpPost("{userId:guid}")]
        public async Task<IActionResult> CreateOrder(Guid userId, CreateOrderDto request, CancellationToken cancellationToken)
        {
            var command = new CreateOrderCommand(userId, request);
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Update status of existing order.
        /// </summary>
        /// <param name="orderId">Unique identifier of the order.</param>
        /// <param name="status">New status of order.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Updated order.</returns>
        [HttpPut("{orderId:guid}")]
        public async Task<IActionResult> UpdateStatus(Guid orderId, OrderStatus status, CancellationToken cancellationToken)
        {
            var command = new ChangeOrderStatusCommand(orderId, status);
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Reject existing order.
        /// </summary>
        /// <param name="orderId">Unique ID of the order.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Result of the cancellation.</returns>
        [HttpPut("reject/{orderId:guid}")]
        public async Task<IActionResult> UpdateStatus(Guid orderId, CancellationToken cancellationToken)
        {
            var command = new CancelOrderCommand(orderId);
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }
    }
}
