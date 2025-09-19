using AutoMapper;
using CatalogService.Application.DTO.Brands;
using CatalogService.Domain.Abstractions.Repositories;
using CatalogService.Domain.Entities;
using MediatR;

namespace CatalogService.Application.Features.Brands.Queries.GetAll
{
    public class GetAllBrandsQueryHandler : IRequestHandler<GetAllBrandsQuery, PagedItems<BrandDto>>
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;
        public GetAllBrandsQueryHandler(IBrandRepository brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }
        public async Task<PagedItems<BrandDto>> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
        {
            var pagedBrands = await _brandRepository.GetPagedAsync(request.PageNumber, request.PageSize, cancellationToken);

            var items = _mapper.Map<PagedItems<BrandDto>>(pagedBrands);

            return items;
        }
    }
}
