using CatalogService.Application.UseCases.Commands.ProductAttributes;
using CatalogService.Domain.Abstractions.Repositories;
using CatalogService.Domain.Exceptions;
using MediatR;

namespace CatalogService.Application.UseCases.CommandHandlers.ProductAttributes
{
    public class DeleteProductAttributeCommandHandler : IRequestHandler<DeleteProductAttributeCommand, Guid>
    {
        private readonly IProductAttributeRepository _productAttributeRepository;
        public DeleteProductAttributeCommandHandler(IProductAttributeRepository productAttributeRepository)
        {
            _productAttributeRepository =  productAttributeRepository;  
        }
        public async Task<Guid> Handle(DeleteProductAttributeCommand request, CancellationToken cancellationToken)
        {
            var existingAttribute = await _productAttributeRepository.GetByIdAsync(request.Id, cancellationToken);

            if(existingAttribute is null)
            {
                throw new NotFoundException($"Attribute with id {request.Id} not found");
            }

            if (existingAttribute.CategoryId != request.CategoryId)
            {
                throw new CategoryMismatchException($"Category {request.CategoryId} not include attribute {request.Id}");
            }

            await _productAttributeRepository.DeleteAsync(existingAttribute, cancellationToken);

            return request.Id;
        }
    }
}
