using CatalogService.Domain.Abstractions.Specification;
using CatalogService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Domain.Abstractions.Repositories
{
    public interface IRepositoryBase<T> where T : class
    {
        Task<T> GetByIdAsync(Guid Id, CancellationToken cancellationToken);
        Task<PagedItems<T>> GetPagedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
        Task UpdateAsync(T Entity, CancellationToken cancellationToken);
        Task DeleteAsync(T Entity, CancellationToken cancellationToken);
        Task AddAsync(T Entity, CancellationToken cancellationToken);
        Task<T?> GetOneBySpecAsync(ISpecification<T> specification, CancellationToken cancellationToken);
        Task<PagedItems<T>> GetPagedBySpecAsync(ISpecification<T> specification, int pageNumber, int pageSize, CancellationToken cancellationToken);
    }
}
