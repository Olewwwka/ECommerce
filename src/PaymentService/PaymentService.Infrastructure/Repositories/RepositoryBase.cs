using MongoDB.Driver;
using PaymentService.Domain.Abstractions;

namespace PaymentService.Infrastructure.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly IMongoCollection<T> _collection;
        protected RepositoryBase(IMongoDatabase database, string collection)
        {
            _collection = database.GetCollection<T>(collection);
        }
        public virtual async Task AddAsync(T entity, CancellationToken cancellationToken)
        {
            await _collection.InsertOneAsync(entity, null, cancellationToken);
        }

        public virtual async Task<Guid> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var filter = Builders<T>.Filter.Eq("Id", id);

            await _collection.DeleteOneAsync(filter, cancellationToken);

            return id;
        }

        public virtual async Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var filter = Builders<T>.Filter.Eq("Id", id);

            var cursor = await _collection.FindAsync(filter, null, cancellationToken);

            var entity = await cursor.FirstOrDefaultAsync(cancellationToken);

            return entity;
        }

        public virtual async Task<List<T>> GetPaginated(int page, int pageSize, CancellationToken cancellationToken)
        {
            var result = await _collection.Find(_ => true)
                .Skip((page - 1) * pageSize)
                .Limit(pageSize)
                .ToListAsync(cancellationToken);

            return result;
        }

        public virtual async Task<T> UpdateAsync(T entity, CancellationToken cancellationToken)
        {
            var idProperty = typeof(T).GetProperty("Id");
                
            var id = (Guid)idProperty.GetValue(entity)!;

            var filter = Builders<T>.Filter.Eq("Id", id);

            var result = await _collection.ReplaceOneAsync(filter, entity, cancellationToken: cancellationToken);

            return entity;
        }
    }
}
