using AutoMapper;
using CatalogService.Application.DTO.Brands;
using CatalogService.Application.Features.Brands.Commands.Create;
using CatalogService.Domain.Entities;

namespace CatalogService.Application.Mappers
{
    public class BrandProfile : Profile
    {
        public BrandProfile()
        {
            CreateMap<CreateBrandCommand, Brand>();
            CreateMap<Brand, BrandDto>();

            CreateMap(typeof(PagedItems<>), typeof(PagedItems<>))
                .ConvertUsing(typeof(PagedItemsProfile<,>));
        }
    }
}
