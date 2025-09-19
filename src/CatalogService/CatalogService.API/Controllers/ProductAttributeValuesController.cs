using CatalogService.Application.DTO.ProductAttributeValues;
using CatalogService.Application.UseCases.Commands.ProductAttributeValues;
using CatalogService.Application.UseCases.Queries.ProductAttributeValues;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.API.Controllers
{
    [ApiController]
    [Route("/products/{productId}/attribute-values")]
    public class ProductAttributeValuesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductAttributeValuesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(Guid productId)
        {
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid productId, Guid id, CancellationToken cancellationToken)
        {
            var query = new GetProductAttributeValueByIdQuery(productId, id);

            var result = await _mediator.Send(query, cancellationToken);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Guid productId, [FromBody]CreateProductAttrbuteValueDto request, CancellationToken cancellationToken)
        {
            var command = new CreateProductAttributeValueCommand(productId, request.ProductAttributeId, request.Value);

            var result = await _mediator.Send(command, cancellationToken);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid productId, Guid id, [FromBody]UpdateProductAttributeValueDto request, CancellationToken cancellationToken)
        {
            var command = new UpdateProductAttributeValueCommand(productId, id, request.Value);

            var result = await _mediator.Send(command, cancellationToken);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid productId, Guid id, CancellationToken cancellationToken)
        {
            var command = new DeleteProductAttributeValueCommand(productId, id);

            var result = await _mediator.Send(command, cancellationToken);

            return Ok(result);
        }
    }
}
