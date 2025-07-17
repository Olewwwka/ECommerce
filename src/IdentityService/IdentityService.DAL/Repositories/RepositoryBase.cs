using IdentityService.DAL.Abstractions;
using IdentityService.DAL.Data;

namespace IdentityService.DAL.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T :class
    {
        protected readonly IdentityServiceDbContext _context;
        protected RepositoryBase(IdentityServiceDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(T entity, CancellationToken cancellationToken)
        {
            await _context.Set<T>().AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity, CancellationToken cancellationToken)
        {
             _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Set<T>().FindAsync(id, cancellationToken);
        }

        public async Task UpdateAsync(T entity, CancellationToken cancellationToken)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
