using CatalogService.Application.UseCases.Commands.Products;
using CatalogService.Domain.Abstractions.Repositories;
using CatalogService.Domain.Exceptions;
using MediatR;

namespace CatalogService.Application.UseCases.CommandHandlers.Products
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Guid>
    {
        private readonly IProductRepository _productRepository;
        public DeleteProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<Guid> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var existingProduct = await _productRepository.GetByIdAsync(request.Id, cancellationToken);

            if (existingProduct is null)
            {
                throw new NotFoundException($"Product with id {request.Id} not found");
            }

            await _productRepository.DeleteAsync(existingProduct, cancellationToken);

            return existingProduct.Id;
        }
    }
}
