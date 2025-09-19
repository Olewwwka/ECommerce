using CatalogService.Domain.Abstractions.Repositories;
using CatalogService.Domain.Abstractions.Specification;
using CatalogService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Infrastructure.Repositories
{
    public class ProductsRepository(CatalogServiceDbContext context) : RepositoryBase<Product>(context), IProductRepository
    {
        public override async Task<PagedItems<Product>> GetPagedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var totalCount = await _context.Products.CountAsync(cancellationToken);

            var items = await _context.Products
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                    .Include(p => p.Category)
                        .ThenInclude(c => c.Attributes)
                    .Include(p => p.AttributeValues)
                        .ThenInclude(a => a.Attribute)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return new PagedItems<Product>(items, totalCount, pageNumber, pageSize);
        }

        public override async Task<Product> GetByIdAsync(Guid Id, CancellationToken cancellationToken)
        {
            return await _context.Products
                .Include(p => p.Category)
                        .ThenInclude(c => c.Attributes)
                .Include(p => p.AttributeValues)
                        .ThenInclude(a => a.Attribute)
                .FirstOrDefaultAsync(p => p.Id == Id, cancellationToken);
        }
        public override async Task<PagedItems<Product>> GetPagedBySpecAsync(ISpecification<Product> specification,
            int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var query = _context.Products
                .Include(p => p.Category)
                        .ThenInclude(c => c.Attributes)
                .Include(p => p.AttributeValues)
                        .ThenInclude(a => a.Attribute)
                .Where(specification.ToExpression());

            var totalCount = await _context.Products.CountAsync(cancellationToken);

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new PagedItems<Product>(items, totalCount, pageNumber, pageSize);
        }
    }
}
