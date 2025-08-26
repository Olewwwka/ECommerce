namespace PaymentService.Domain.Abstractions
{
    public interface IRepositoryBase<T>
    {
        Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task AddAsync(T entity, CancellationToken cancellationToken);
        Task<Guid> DeleteAsync(Guid id, CancellationToken cancellationToken);
        Task<T> UpdateAsync(T entity, CancellationToken cancellationToken);
        Task<List<T>> GetPaginated(int  page, int pageSize, CancellationToken cancellationToken);
    }
}
