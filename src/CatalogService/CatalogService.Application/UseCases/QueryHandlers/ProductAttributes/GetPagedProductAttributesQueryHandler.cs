using AutoMapper;
using CatalogService.Application.DTO.ProductAttributes;
using CatalogService.Application.UseCases.Queries.ProductAttributes;
using CatalogService.Domain.Abstractions.Repositories;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Exceptions;
using MediatR;

namespace CatalogService.Application.UseCases.QueryHandlers.ProductAttributes
{
    public class GetPagedProductAttributesQueryHandler : IRequestHandler<GetPagedProductAttributesQuery, PagedItems<ProductAttributeDto>>
    {
        private readonly IProductAttributeRepository _productAttributeRepository;
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;
        public GetPagedProductAttributesQueryHandler(IProductAttributeRepository productAttributeRepository, IMapper mapper, ICategoryRepository categoryRepository)
        {
            _productAttributeRepository = productAttributeRepository;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }
        public async Task<PagedItems<ProductAttributeDto>> Handle(GetPagedProductAttributesQuery request, CancellationToken cancellationToken)
        {
            var existringCategory = await _categoryRepository.GetByIdAsync(request.CategoryId, cancellationToken);

            if (existringCategory is null)
            {
                throw new NotFoundException($"Category with id {request.CategoryId} not found");
            }

            var productAttributes = await _productAttributeRepository.GetPagedByCategoryAsync(existringCategory.Id, request.PageNumber, request.PageSize, cancellationToken);

            var items = _mapper.Map<PagedItems<ProductAttributeDto>>(productAttributes);

            return items;
        }
    }
}
