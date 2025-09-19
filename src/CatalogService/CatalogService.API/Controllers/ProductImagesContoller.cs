using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.API.Controllers
{
    [ApiController]
    [Route("/products/{productId}/images")]
    public class ProductImagesContoller : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductImagesContoller(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid productId, Guid id)
        {
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(Guid productId)
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Guid productId)
        {
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid productId, Guid id)
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid productId, Guid id)
        {
            return Ok();
        }
    }
}



