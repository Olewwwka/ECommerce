using CatalogService.Domain.Abstractions.Repositories;
using CatalogService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Infrastructure.Repositories
{
    public class ProductAttributeRepository(CatalogServiceDbContext context) : RepositoryBase<ProductAttribute>(context), IProductAttributeRepository
    {
        public async Task<PagedItems<ProductAttribute>> GetPagedByCategoryAsync(Guid categoryId, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var totalCount = await _context.Set<ProductAttribute>().CountAsync(cancellationToken);

            var items = await _context.Set<ProductAttribute>()
                .Where(a => a.CategoryId == categoryId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return new PagedItems<ProductAttribute>(items, totalCount, pageNumber, pageSize);
        }
    }
}
