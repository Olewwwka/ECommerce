using AutoMapper;
using CatalogService.Domain.Abstractions.Repositories;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Exceptions;
using CatalogService.Domain.Specifications.Application;
using CatalogService.Domain.Specifications.Custom;
using CatalogService.Domain.Specifications.Infrastructure;
using MediatR;

namespace CatalogService.Application.Features.ProductAttributesValues.Commands.Create
{
    public class CreateProductAttributeValueHandler : IRequestHandler<CreateProductAttributeValueCommand, string>
    {
        private readonly IProductAttributeValueRepository _productAttributeValueRepository;
        private readonly IProductAttributeRepository _productAttributeRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CreateProductAttributeValueHandler(IProductAttributeValueRepository productAttributeValueRepository, 
            ICategoryRepository categoryRepository,
            IMapper mapper, 
            IProductAttributeRepository productAttributeRepository,
            IProductRepository productRepository)
        {
            _productAttributeValueRepository = productAttributeValueRepository;
            _mapper = mapper;
            _productAttributeRepository = productAttributeRepository;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<string> Handle(CreateProductAttributeValueCommand request, CancellationToken cancellationToken)
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

            if(existingProductAttributeValue is not null)
            {
                throw new AlreadyExistsException($"Product with id {request.ProductId} already has value to attribute {request.ProductAttributeId}");
            }

            var categoryHasAttribute = new CategoryHasAttributeSpecification(request.ProductAttributeId);

            var categoryWithAttribute = await _categoryRepository.GetOneBySpecAsync(categoryHasAttribute, cancellationToken);

            if(categoryWithAttribute is null)
            {
                throw new CategoryMismatchException($"Wrong attribute");
            }

            var valueExistsSpec = new ValueAlreadyExistsSpecification(request.Value, request.ProductAttributeId);

            var existingValue = await _productAttributeValueRepository.GetOneBySpecAsync(valueExistsSpec, cancellationToken);

            if(existingValue is not null)
            {
                var reusedAttributeValue = new ProductAttributeValue
                {
                    ProductId = request.ProductId,
                    ProductAttributeId = request.ProductAttributeId,
                    Value = existingValue.Value 
                };

                await _productAttributeValueRepository.AddAsync(reusedAttributeValue, cancellationToken);

                return reusedAttributeValue.Value;
            }
            else
            {
                var attributeValue = _mapper.Map<ProductAttributeValue>(request);

                await _productAttributeValueRepository.AddAsync(attributeValue, cancellationToken);

                return attributeValue.Value;
            }
        }
    }
}
