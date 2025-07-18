using AutoMapper;
using CatalogService.Application.UseCases.Commands.ProductAttributes;
using CatalogService.Domain.Abstractions.Repositories;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Exceptions;
using MediatR;

namespace CatalogService.Application.UseCases.CommandHandlers.ProductAttributes
{
    public class CreateProductAttributeCommandHandler : IRequestHandler<CreateProductAttributeCommand, string>
    {
        private readonly IProductAttributeRepository _productAttributeRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CreateProductAttributeCommandHandler(IProductAttributeRepository productAttributeRepository, IMapper mapper, ICategoryRepository categoryRepository)
        {
            _productAttributeRepository = productAttributeRepository;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }
        public async Task<string> Handle(CreateProductAttributeCommand request, CancellationToken cancellationToken)
        {
            var existingCategory = await _categoryRepository.GetByIdAsync(request.CategoryId, cancellationToken);

            if(existingCategory is null)
            {
                throw new NotFoundException($"Category with id {request.CategoryId} not found");
            }

            if(existingCategory.Attributes.Any(a => a.Name == request.Name))
            {
                throw new Exception();
            }

            var productAttribute = _mapper.Map<ProductAttribute>(request);

            await _productAttributeRepository.AddAsync(productAttribute, cancellationToken);

            return productAttribute.Name;
        }
    }
}
