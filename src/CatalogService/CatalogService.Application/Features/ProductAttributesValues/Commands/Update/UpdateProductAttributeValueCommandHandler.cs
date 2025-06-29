using AutoMapper;
using CatalogService.Domain.Abstractions.Repositories;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Exceptions;
using CatalogService.Domain.Specifications.Application;
using CatalogService.Domain.Specifications.Custom;
using CatalogService.Domain.Specifications.Infrastructure;
using MediatR;

namespace CatalogService.Application.Features.ProductAttributesValues.Commands.Update
{
    public class UpdateProductAttributeValueCommandHandler : IRequestHandler<UpdateProductAttributeValueCommand, Guid>
    {
        private readonly IProductAttributeValueRepository _productAttributeValueRepository;
        private readonly IProductAttributeRepository _productAttributeRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public UpdateProductAttributeValueCommandHandler(IProductAttributeValueRepository productAttributeValueRepository,
            ICategoryRepository categoryRepository,
            IProductAttributeRepository productAttributeRepository,
            IProductRepository productRepository)
        {
            _productAttributeValueRepository = productAttributeValueRepository;
            _productAttributeRepository = productAttributeRepository;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }


        public async Task<Guid> Handle(UpdateProductAttributeValueCommand request, CancellationToken cancellationToken)
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

            var categoryHasAttribute = new CategoryHasAttributeSpecification(request.ProductAttributeId);

            var categoryWithAttribute = await _categoryRepository.GetOneBySpecAsync(categoryHasAttribute, cancellationToken);

            if (categoryWithAttribute is null)
            {
                throw new CategoryMismatchException($"Wrong attribute");
            }

            var productHasValueSpec = new AttributeValueByProductIdSpecification(request.ProductId, request.ProductAttributeId);

            var existingProductAttributeValue = await _productAttributeValueRepository.GetOneBySpecAsync(productHasValueSpec, cancellationToken);

            if (existingProductAttributeValue is null)
            {
                throw new AlreadyExistsException($"Product with id {request.ProductId} does not have value to attribute {request.ProductAttributeId}");
            }
            

            var valueExistsSpec = new ValueAlreadyExistsSpecification(request.Value, request.ProductAttributeId);

            var existingValue = await _productAttributeValueRepository.GetOneBySpecAsync(valueExistsSpec, cancellationToken);

            if (existingValue is  not null)
            {
                existingProductAttributeValue.Value = existingValue.Value;
            }
            else
            {
                existingProductAttributeValue.Value = request.Value;
            }

            await _productAttributeValueRepository.UpdateAsync(existingProductAttributeValue, cancellationToken);

            return existingProductAttributeValue.Id;
        }
    }
}
