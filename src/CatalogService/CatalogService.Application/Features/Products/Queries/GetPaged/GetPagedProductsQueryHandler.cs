using AutoMapper;
using CatalogService.Application.DTO.Product;
using CatalogService.Domain.Abstractions.Repositories;
using CatalogService.Domain.Entities;
using MediatR;

namespace CatalogService.Application.Features.Products.Queries.GetPaged
{
    public class GetPagedProductsQueryHandler : IRequestHandler<GetPagedProductsQuery, PagedItems<ProductDto>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public GetPagedProductsQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<PagedItems<ProductDto>> Handle(GetPagedProductsQuery request, CancellationToken cancellationToken)
        {
            var pagedProducts = await _productRepository.GetPagedAsync(request.PageNumber, request.PageSize, cancellationToken);

            var items = _mapper.Map<PagedItems<ProductDto>>(pagedProducts);

            return items;
        }
    }
}
