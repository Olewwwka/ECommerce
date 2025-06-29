using AutoMapper;
using CatalogService.Application.DTO.Brands;
using CatalogService.Domain.Abstractions.Repositories;
using CatalogService.Domain.Exceptions;
using MediatR;

namespace CatalogService.Application.Features.Brands.Queries.GetById
{
    public class GetBrandByIdQueryHandler : IRequestHandler<GetBrandByIdQuery, BrandDto>
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;
        public GetBrandByIdQueryHandler(IBrandRepository brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }
        public async Task<BrandDto> Handle(GetBrandByIdQuery request, CancellationToken cancellationToken)
        {
            var existingBrand = await _brandRepository.GetByIdAsync(request.Id, cancellationToken);

            if(existingBrand is null)
            {
                throw new NotFoundException($"Brand with id {request.Id} not found");
            }

            var brand = _mapper.Map<BrandDto>(existingBrand);

            return brand;
        }
    }
}
