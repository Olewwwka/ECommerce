using AutoMapper;
using CatalogService.Application.DTO.Product;
using CatalogService.Application.UseCases.Queries.Products;
using CatalogService.Domain.Abstractions.Repositories;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Specifications.Custom;
using MediatR;

namespace CatalogService.Application.UseCases.QueryHandlers.Products
{
    public class GetFilteredProductsQueryHandler : IRequestHandler<GetFilteredProductsQuery, PagedItems<ProductDto>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public GetFilteredProductsQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<PagedItems<ProductDto>> Handle(GetFilteredProductsQuery request, CancellationToken cancellationToken)
        {
            var filter = _mapper.Map<ProductFilter>(request.filter);

            var spec = new ProductFilterSpecification(filter);

            var products = await _productRepository.GetPagedBySpecAsync(spec, request.options.PageNumber, request.options.PageSize, cancellationToken);

            var items = _mapper.Map<PagedItems<ProductDto>>(products);

            return items;
        }
    }
}
