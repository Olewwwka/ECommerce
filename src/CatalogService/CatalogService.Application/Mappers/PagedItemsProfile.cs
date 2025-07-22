using AutoMapper;
using CatalogService.Domain.Entities;

namespace CatalogService.Application.Mappers
{
    public class PagedItemsProfile<TSource, TDest> : ITypeConverter<PagedItems<TSource>, PagedItems<TDest>> where TDest : class where TSource : class
    {
        public PagedItems<TDest> Convert(PagedItems<TSource> source, PagedItems<TDest> destination, ResolutionContext context)
        {
            return new PagedItems<TDest>(
                context.Mapper.Map<List<TDest>>(source.Items),
                source.PageSize,
                source.PageNumber,
                source.TotalCount);
        }
    }
}
