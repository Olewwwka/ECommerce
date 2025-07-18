using Microsoft.AspNetCore.Mvc;
using PaymentService.Application.Abstractions;

namespace PaymentService.API.Controllers
{
    [ApiController]
    [Route("receipts")]
    public class ReceiptController : ControllerBase
    {
        private readonly IReceiptService _receiptService;
        public ReceiptController(IReceiptService receiptService)
        {
            _receiptService = receiptService;
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await _receiptService.GetByIdAsync(id, cancellationToken);

            return Ok(result);
        }

        [HttpGet("order/{orderId:guid}")]
        public async Task<IActionResult> GetByOrderId(Guid orderId, CancellationToken cancellationToken)
        {
            var result = await _receiptService.GetByOrderId(orderId, cancellationToken);

            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var result = await _receiptService.GetAll(pageNumber, pageSize, cancellationToken);

            return Ok(result);
        }
    }
}
