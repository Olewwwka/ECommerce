using AutoMapper;
using PaymentService.Application.Abstractions;
using PaymentService.Application.DTO;
using PaymentService.Domain.Abstractions;
using PaymentService.Domain.Entities;
using PaymentService.Domain.Exceptions;

namespace PaymentService.Application.Services
{
    public class ReceiptService : IReceiptService
    {
        private readonly IMapper _mapper;
        private readonly IReceiptRepository _repository;
        public ReceiptService(IMapper mapper, IReceiptRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<ReceiptDTO> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var receipt = await _repository.GetByIdAsync(id, cancellationToken);

            if(receipt is null)
            {
                throw new NotFoundException($"Receipt with ID {id} not found");
            }

            var result = _mapper.Map<ReceiptDTO>(receipt);

            return result;
        }
        public async Task<ReceiptDTO> GetByOrderId(Guid orderId, CancellationToken cancellationToken)
        {
            var receipt = await _repository.GetByOrderIdAsync(orderId, cancellationToken);

            if (receipt is null)
            {
                throw new NotFoundException($"Receipt for order with ID {orderId} not found");
            }

            var result = _mapper.Map<ReceiptDTO>(receipt);

            return result;
        }

        public async Task<List<ReceiptDTO>> GetAll(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var receipts = await _repository.GetPaginated(pageNumber, pageSize, cancellationToken);

            var result = _mapper.Map<List<ReceiptDTO>>(receipts);

            return result;
        }
    }
}
