﻿using CatalogService.Application.DTO.Categories;
using CatalogService.Application.Features.Categories.Commands.Create;
using CatalogService.Application.Features.Categories.Commands.Delete;
using CatalogService.Application.Features.Categories.Commands.Update;
using CatalogService.Application.Features.Categories.Queries.GetAll;
using CatalogService.Application.Features.Categories.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.API.Controllers
{
    [ApiController]
    [Route("/categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var request = new GetCategoryByIdQuery(id);

            var result = await _mediator.Send(request, cancellationToken);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetPagedCategoriesQuery request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var command = new DeleteCategoryCommand(id);

            var result = await _mediator.Send(command, cancellationToken);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody]UpdateCategoryDto request, CancellationToken cancellationToken)
        {
            var command = new UpdateCategoryCommand(id, request.Name);

            var result = await _mediator.Send(command, cancellationToken);

            return Ok(result);
        }
    }
}
