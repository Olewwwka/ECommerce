using BasketService.Application.DTO;
using BasketService.Application.UseCases.Commands.Baskets;
using BasketService.Application.UseCases.Queries.Baskets;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace BasketService.API.Controllers
{
    [ApiController]
    [Route("/baskets")]
    public class BasketsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public BasketsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{userId:guid}")]
        public async Task<IActionResult> GetByUserId(Guid userId, CancellationToken cancellationToken)
        {
            var query = new GetBasketByUserIdQuery(userId);

            var result = await _mediator.Send(query, cancellationToken);

            return Ok(result);
        }

        [HttpDelete("{userId:guid}")]
        public async Task<IActionResult> Delete(Guid userId, CancellationToken cancellationToken)
        {
            var command = new DeleteBasketCommand(userId);

            var result = await _mediator.Send(command, cancellationToken);

            return Ok(result);
        }

        [HttpPut("{userId:guid}")]
        public async Task<IActionResult> Update(Guid userId, BasketDto request, CancellationToken cancellationToken)
        {
            var command = new UpdateBasketCommand(userId, request);

            var result = await _mediator.Send(command, cancellationToken);

            return Ok(result);
        }
        [HttpGet("exception")]
        public async Task<IActionResult> Test()
        {
            throw new RedisConnectionException(ConnectionFailureType.UnableToConnect, "ex");
        }
    }
}
