using System.Linq.Expressions;

namespace Leaguelane.Repository.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        T GetById(object id);
        IQueryable<T> GetAll();
        IQueryable<T> FindAll(Expression<Func<T, bool>> predicate);
        T FirstOrDefault(Expression<Func<T, bool>> predicate);

        // Async query methods
        Task<T> GetByIdAsync(object id, CancellationToken cancellationToken = default);
        Task<IQueryable<T>> GetAllAsync();
        Task<IQueryable<T>> FindAllAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

        // Synchronous CRUD operations
        T Add(T entity);
        IEnumerable<T> AddRange(IEnumerable<T> entities);
        void Update(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);

        // Async CRUD operations
        Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
        Task UpdateAsync(T entity);
        Task RemoveAsync(T entity);
        Task RemoveRangeAsync(IEnumerable<T> entities);

        // Count
        int Count(Expression<Func<T, bool>> predicate = null);
        Task<int> CountAsync(Expression<Func<T, bool>> predicate = null, CancellationToken cancellationToken = default);

        // Exists
        bool Exists(Expression<Func<T, bool>> predicate);
        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
