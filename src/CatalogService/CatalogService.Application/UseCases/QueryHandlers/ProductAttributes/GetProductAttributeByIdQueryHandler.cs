using AutoMapper;
using CatalogService.Application.DTO.ProductAttributes;
using CatalogService.Application.UseCases.Queries.ProductAttributes;
using CatalogService.Domain.Abstractions.Repositories;
using CatalogService.Domain.Exceptions;
using MediatR;

namespace CatalogService.Application.UseCases.QueryHandlers.ProductAttributes
{
    public class GetProductAttributeByIdQueryHandler : IRequestHandler<GetProductAttributeByIdQuery, ProductAttributeDto>
    {
        private readonly IProductAttributeRepository _productAttributeRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public GetProductAttributeByIdQueryHandler(IProductAttributeRepository productAttributeRepository, IMapper mapper, ICategoryRepository categoryRepository)
        {
            _productAttributeRepository = productAttributeRepository;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }
        public async Task<ProductAttributeDto> Handle(GetProductAttributeByIdQuery request, CancellationToken cancellationToken)
        {
            var existingAttribute = await _productAttributeRepository.GetByIdAsync(request.Id, cancellationToken);

            if (existingAttribute is null)
            {
                throw new NotFoundException($"Attribute with id {request.Id} not found");
            }

            var existringCategory = await _categoryRepository.GetByIdAsync(request.CategoryId, cancellationToken);

            if (existringCategory is null)
            {
                throw new NotFoundException($"Category with id {request.CategoryId} not found");
            }

            if (existingAttribute.CategoryId != request.CategoryId)
            {
                throw new CategoryMismatchException($"Category {request.CategoryId} not include attribute {request.Id}");
            }

            var attribute = _mapper.Map<ProductAttributeDto>(existingAttribute);

            return attribute;
        }
    }
}
