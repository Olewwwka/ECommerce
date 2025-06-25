using CatalogService.Domain.Abstractions.Repositories;
using CatalogService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Infrastructure.Repositories
{
    public class CategoryRepository(CatalogServiceDbContext context) : RepositoryBase<Category>(context), ICategoryRepository
    {
        public override async Task<PagedItems<Category>> GetPagedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var totalCount = await _context.Set<Category>().CountAsync(cancellationToken);

            var items = await _context.Set<Category>()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Include(c => c.Attributes)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return new PagedItems<Category>(items, totalCount, pageNumber, pageSize);
        }
        public override async Task<Category> GetByIdAsync(Guid Id, CancellationToken cancellationToken)
        {
            return await _context.Set<Category>().Include(c => c.Attributes).FirstOrDefaultAsync(c => c.Id == Id, cancellationToken);
        }
    }
}
