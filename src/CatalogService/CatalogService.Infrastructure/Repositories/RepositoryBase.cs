using CatalogService.Domain.Abstractions.Repositories;
using CatalogService.Domain.Abstractions.Specification;
using CatalogService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Infrastructure.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected readonly CatalogServiceDbContext _context;
        public RepositoryBase(CatalogServiceDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(T Entity, CancellationToken cancellationToken)
        {
            await _context.Set<T>().AddAsync(Entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(T Entity, CancellationToken cancellationToken)
        {
            _context.Set<T>().Remove(Entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task<PagedItems<T>> GetPagedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var totalCount = await _context.Set<T>().CountAsync(cancellationToken);

            var items = await _context.Set<T>()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return new PagedItems<T>(items, totalCount, pageNumber, pageSize);
        }
        public virtual async Task<T> GetByIdAsync(Guid Id, CancellationToken cancellationToken)
        {
            return await _context.Set<T>().FindAsync(Id, cancellationToken);
        }

        public async Task UpdateAsync(T Entity, CancellationToken cancellationToken)
        {
            _context.Set<T>().Update(Entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<T?> GetOneBySpecAsync(ISpecification<T> specification, CancellationToken cancellationToken)
        {
            return await _context.Set<T>()
                .Where(specification.ToExpression())
                .SingleOrDefaultAsync(cancellationToken);
        }

        public virtual async Task<PagedItems<T>> GetPagedBySpecAsync(ISpecification<T> specification, 
            int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var query = _context.Set<T>().Where(specification.ToExpression());

            var totalCount = await _context.Set<T>().CountAsync(cancellationToken);

            var items =  await query
                .Skip((pageNumber-1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new PagedItems<T>(items, totalCount, pageNumber, pageSize);
        }
    }
}
