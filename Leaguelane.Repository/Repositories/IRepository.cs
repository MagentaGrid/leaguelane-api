using System.Linq.Expressions;

namespace Leaguelane.Repository.Repositories
{
    public interface IRepository
    {
        T GetById<T>(object id) where T : class;
        IQueryable<T> GetAll<T>() where T : class;
        IQueryable<T> FindAll<T>(Expression<Func<T, bool>> predicate) where T : class;
        T FirstOrDefault<T>(Expression<Func<T, bool>> predicate) where T : class;

        // Async query methods
        Task<T> GetByIdAsync<T>(object id, CancellationToken cancellationToken = default) where T : class;
        Task<IQueryable<T>> GetAllAsync<T>() where T : class;
        Task<IQueryable<T>> FindAllAsync<T>(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default) where T : class;
        Task<T> FirstOrDefaultAsync<T>(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default) where T : class;

        // Synchronous CRUD operations
        T Add<T>(T entity) where T : class;
        IEnumerable<T> AddRange<T>(IEnumerable<T> entities) where T : class;
        void Update<T>(T entity) where T : class;
        void Remove<T>(T entity) where T : class;
        void RemoveRange<T>(IEnumerable<T> entities) where T : class;

        // Async CRUD operations
        Task<T> AddAsync<T>(T entity, CancellationToken cancellationToken = default) where T : class;
        Task<IEnumerable<T>> AddRangeAsync<T>(IEnumerable<T> entities, CancellationToken cancellationToken = default) where T : class;
        Task UpdateAsync<T>(T entity) where T : class;
        Task RemoveAsync<T>(T entity) where T : class;
        Task RemoveRangeAsync<T>(IEnumerable<T> entities) where T : class;

        // Count
        int Count<T>(Expression<Func<T, bool>> predicate = null) where T : class;
        Task<int> CountAsync<T>(Expression<Func<T, bool>> predicate = null, CancellationToken cancellationToken = default) where T : class;

        // Exists
        bool Exists<T>(Expression<Func<T, bool>> predicate) where T : class;
        Task<bool> ExistsAsync<T>(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default) where T : class;
        int SaveChanges<T>() where T : class;
        Task<int> SaveChangesAsync<T>(CancellationToken cancellationToken = default) where T : class;
    }
}
