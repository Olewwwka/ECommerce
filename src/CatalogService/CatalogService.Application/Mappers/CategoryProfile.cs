using AutoMapper;
using CatalogService.Application.DTO.Categories;
using CatalogService.Application.Features.Categories.Commands.Create;
using CatalogService.Domain.Entities;

namespace CatalogService.Application.Mappers
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CreateCategoryCommand, Category>();
            CreateMap<Category, CategoryWithAttributesDto>();

            CreateMap(typeof(PagedItems<>), typeof(PagedItems<>))
                .ConvertUsing(typeof(PagedItemsProfile<,>));
        }
    }
}
