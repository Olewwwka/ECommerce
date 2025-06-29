using CatalogService.Domain.Abstractions.Repositories;

namespace CatalogService.Infrastructure.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly CatalogServiceDbContext _context;
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

        public async Task<T> GetByIdAsync(Guid Id, CancellationToken cancellationToken)
        {
            return await _context.Set<T>().FindAsync(Id, cancellationToken);
        }

        public async Task UpdateAsync(T Entity, CancellationToken cancellationToken)
        {
            _context.Set<T>().Update(Entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
