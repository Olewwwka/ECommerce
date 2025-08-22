using CatalogService.Application.DTO;
using CatalogService.Application.DTO.ProductAttributes;
using CatalogService.Application.UseCases.Commands.ProductAttributes;
using CatalogService.Application.UseCases.Queries.ProductAttributes;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.API.Controllers
{
    [ApiController]
    [Route("/categories/{categoryId}/attributes")]
    public class CategoryAttributesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CategoryAttributesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Guid categoryId, [FromBody]CreateProductAttributeDto request, CancellationToken cancellationToken)
        {
            var command = new CreateProductAttributeCommand(categoryId, request.Name, request.DataType);

            var result = await _mediator.Send(command, cancellationToken);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid categoryId, Guid id, [FromBody]UpdateProductAttributeDto request, CancellationToken cancellationToken)
        {
            var command = new UpdateProductAttributeCommand(categoryId, id, request.Name, request.DataType);

            var result = await _mediator.Send(command, cancellationToken);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid categoryId, Guid id, CancellationToken cancellationToken)
        {
            var command = new DeleteProductAttributeCommand(categoryId, id);

            var result = await _mediator.Send(command, cancellationToken);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid categoryId, Guid id, CancellationToken cancellationToken)
        {
            var query = new GetProductAttributeByIdQuery(categoryId, id);

            var result = await _mediator.Send(query, cancellationToken);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(Guid categoryId, [FromQuery]GetPagedItemsDto request, CancellationToken cancellationToken)
        {
            var query = new GetPagedProductAttributesQuery(categoryId, request.PageNumber, request.PageSize);

            var result = await _mediator.Send(query, cancellationToken);

            return Ok(result);
        }
    }
}
