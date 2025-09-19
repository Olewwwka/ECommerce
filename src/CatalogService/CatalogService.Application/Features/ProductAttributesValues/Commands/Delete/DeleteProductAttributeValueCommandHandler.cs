using AutoMapper;
using CatalogService.Application.Features.ProductAttributesValues.Commands.Create;
using CatalogService.Domain.Abstractions.Repositories;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Exceptions;
using CatalogService.Domain.Specifications.Application;
using CatalogService.Domain.Specifications.Custom;
using CatalogService.Domain.Specifications.Infrastructure;
using MediatR;

namespace CatalogService.Application.Features.ProductAttributesValues.Commands.Delete
{
    public class DeleteProductAttributeValueCommandHandler : IRequestHandler<DeleteProductAttributeValueCommand, bool>
    {
        private readonly IProductAttributeValueRepository _productAttributeValueRepository;
        private readonly IProductAttributeRepository _productAttributeRepository;
        private readonly IProductRepository _productRepository;

        public DeleteProductAttributeValueCommandHandler(IProductAttributeValueRepository productAttributeValueRepository,
            IProductAttributeRepository productAttributeRepository,
            IProductRepository productRepository)
        {
            _productAttributeValueRepository = productAttributeValueRepository;
            _productAttributeRepository = productAttributeRepository;
            _productRepository = productRepository;
        }

        public async Task<bool> Handle(DeleteProductAttributeValueCommand request, CancellationToken cancellationToken)
        {
            var existingProduct = await _productRepository.GetByIdAsync(request.ProductId, cancellationToken);

            if (existingProduct is null)
            {
                throw new NotFoundException($"Product with id {request.ProductId} not found");
            }

            var attribute = await _productAttributeRepository.GetByIdAsync(request.ProductAttributeId, cancellationToken);

            if (attribute is null)
            {
                throw new NotFoundException($"Attribute with id {request.ProductAttributeId} not found");
            }

            var productHasValueSpec = new AttributeValueByProductIdSpecification(request.ProductId, request.ProductAttributeId);

            var existingProductAttributeValue = await _productAttributeValueRepository.GetOneBySpecAsync(productHasValueSpec, cancellationToken);


            if(existingProductAttributeValue is null)
            {
                throw new NotFoundException($"Product with id {request.ProductId} dosn't have value for attribute {request.ProductAttributeId}");
            }

            await _productAttributeValueRepository.DeleteAsync(existingProductAttributeValue, cancellationToken);

            return true;
        }
    }
}
