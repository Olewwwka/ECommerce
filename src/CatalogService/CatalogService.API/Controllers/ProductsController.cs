using CatalogService.Application.DTO;
using CatalogService.Application.DTO.Product;
using CatalogService.Application.UseCases.Commands.Products;
using CatalogService.Application.UseCases.Queries.Products;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.API.Controllers
{
    [ApiController]
    [Route("/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductCommand request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetPaginated([FromQuery]GetPagedProductsQuery request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetProductByIdQuery(id);

            var result = await _mediator.Send(query, cancellationToken);

            return Ok(result);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody]UpdateProductDto request, CancellationToken cancellationToken)
        {
            var command = new UpdateProductCommand(id, request);

            var result = await _mediator.Send(command, cancellationToken);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var command = new DeleteProductCommand(id);

            var result = await _mediator.Send(command, cancellationToken);

            return Ok(result);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetFiltered([FromQuery]GetPagedItemsDto options, [FromQuery]ProductFilterDto request, CancellationToken cancellationToken)
        {
            var query = new GetFilteredProductsQuery(options, request);

            var result = await _mediator.Send(query, cancellationToken);

            return Ok(result);
        }
    }
}
