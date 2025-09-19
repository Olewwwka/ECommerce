using CatalogService.Domain.Entities;

namespace CatalogService.Domain.Abstractions.Repositories
{
    public interface IProductAttributeRepository : IRepositoryBase<ProductAttribute>
    {
        Task<PagedItems<ProductAttribute>> GetPagedByCategoryAsync(Guid categoryId, int pageNumber, int pageSize, CancellationToken cancellationToken);
    }
}
