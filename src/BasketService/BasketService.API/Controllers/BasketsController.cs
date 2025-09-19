using BasketService.Application.DTO;
using BasketService.Application.UseCases.Commands.Baskets;
using BasketService.Application.UseCases.Queries.Baskets;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BasketService.API.Controllers
{
    /// <summary>
    /// Сontroller for managing user baskets.
    /// Provides endpoints to get, update, delete, and modify basket items.
    /// </summary>
    [ApiController]
    [Route("baskets")]
    public class BasketsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BasketsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get basket for user by user ID.
        /// </summary>
        /// <param name="userId">Unique ID of user.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Basket of user.</returns>
        [HttpGet("{userId:guid}")]
        public async Task<IActionResult> GetByUserId(Guid userId, CancellationToken cancellationToken)
        {
            var query = new GetBasketByUserIdQuery(userId);

            var result = await _mediator.Send(query, cancellationToken);

            return Ok(result);
        }

        /// <summary>
        /// Delete basket for specific user by user ID.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The result of the delete operation.</returns>
        [HttpDelete("{userId:guid}")]
        public async Task<IActionResult> Delete(Guid userId, CancellationToken cancellationToken)
        {
            var command = new DeleteBasketCommand(userId);

            var result = await _mediator.Send(command, cancellationToken);

            return Ok(result);
        }

        /// <summary>
        /// Update basket for specific user ID.
        /// </summary>
        /// <param name="userId">Unique ID of user.</param>
        /// <param name="request">Updated basket data.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Updated basket.</returns>
        [HttpPut("{userId:guid}")]
        public async Task<IActionResult> Update(Guid userId, BasketDto request, CancellationToken cancellationToken)
        {
            var command = new UpdateBasketCommand(userId, request);

            var result = await _mediator.Send(command, cancellationToken);

            return Ok(result);
        }

        /// <summary>
        /// Add item to user basket.
        /// </summary>
        /// <param name="userId">Unique ID of user.</param>
        /// <param name="productId">Unique ID of product to add.</param>
        /// <param name="quantity">Quantity of the product to add.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The updated basket with the new item.</returns>
        [HttpPost("{userId:guid}/items")]
        public async Task<IActionResult> AddItemToBasket(Guid userId, Guid productId, int quantity, CancellationToken cancellationToken)
        {
            var command = new AddItemToBasketCommand(userId, productId, quantity);

            var result = await _mediator.Send(command, cancellationToken);

            return Ok(result);
        }
    }
}
