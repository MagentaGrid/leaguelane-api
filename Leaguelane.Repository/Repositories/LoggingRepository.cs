using Leaguelane.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Leaguelane.Repository.Repositories
{
    public class LoggingRepository: ILoggingRepository
    {
        private readonly LoggingDbContext _context;

        public LoggingRepository(LoggingDbContext context)
        {
            _context = context;
        }
        public virtual T GetById<T>(object id) where T : class
        {
            return _context.Set<T>().Find(id);
        }

        public virtual IQueryable<T> GetAll<T>() where T : class
        {
            return _context.Set<T>().AsQueryable();
        }

        public virtual IQueryable<T> FindAll<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return _context.Set<T>().Where(predicate);
        }

        public virtual T FirstOrDefault<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return _context.Set<T>().FirstOrDefault(predicate);
        }

        // Async query methods
        public virtual async Task<T> GetByIdAsync<T>(object id, CancellationToken cancellationToken = default) where T : class
        {
            return await _context.Set<T>().FindAsync(id, cancellationToken);
        }

        public virtual Task<IQueryable<T>> GetAllAsync<T>() where T : class
        {
            return Task.FromResult(_context.Set<T>().AsQueryable());
        }

        public virtual Task<IQueryable<T>> FindAllAsync<T>(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default) where T : class
        {
            return Task.FromResult(_context.Set<T>().Where(predicate));
        }

        public virtual async Task<T> FirstOrDefaultAsync<T>(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default) where T : class
        {
            return await _context.Set<T>().FirstOrDefaultAsync(predicate, cancellationToken);
        }

        // Synchronous CRUD operations
        public virtual T Add<T>(T entity) where T : class
        {
            var entry = _context.Set<T>().Add(entity);
            return entry.Entity;
        }

        public virtual IEnumerable<T> AddRange<T>(IEnumerable<T> entities) where T : class
        {
            var entityList = entities.ToList();
            _context.Set<T>().AddRange(entityList);
            return entityList;
        }

        public virtual void Update<T>(T entity) where T : class
        {
            _context.Set<T>().Update(entity);
        }

        public virtual void Remove<T>(T entity) where T : class
        {
            _context.Set<T>().Remove(entity);
        }

        public virtual void RemoveRange<T>(IEnumerable<T> entities) where T : class
        {
            _context.Set<T>().RemoveRange(entities);
        }

        // Async CRUD operations
        public virtual async Task<T> AddAsync<T>(T entity, CancellationToken cancellationToken = default) where T : class
        {
            var entry = await _context.Set<T>().AddAsync(entity, cancellationToken);
            return entry.Entity;
        }

        public virtual async Task<IEnumerable<T>> AddRangeAsync<T>(IEnumerable<T> entities, CancellationToken cancellationToken = default) where T : class
        {
            var entityList = entities.ToList();
            await _context.Set<T>().AddRangeAsync(entityList, cancellationToken);
            return entityList;
        }

        public virtual Task UpdateAsync<T>(T entity) where T : class
        {
            _context.Set<T>().Update(entity);
            return Task.CompletedTask;
        }

        public virtual Task RemoveAsync<T>(T entity) where T : class
        {
            _context.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }

        public virtual Task RemoveRangeAsync<T>(IEnumerable<T> entities) where T : class
        {
            _context.Set<T>().RemoveRange(entities);
            return Task.CompletedTask;
        }

        // Count
        public virtual int Count<T>(Expression<Func<T, bool>> predicate = null) where T : class
        {
            return predicate == null ? _context.Set<T>().Count() : _context.Set<T>().Count(predicate);
        }

        public virtual async Task<int> CountAsync<T>(Expression<Func<T, bool>> predicate = null, CancellationToken cancellationToken = default) where T : class
        {
            return predicate == null ? await _context.Set<T>().CountAsync(cancellationToken) : await _context.Set<T>().CountAsync(predicate, cancellationToken);
        }

        // Exists
        public virtual bool Exists<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return _context.Set<T>().Any(predicate);
        }

        public virtual async Task<bool> ExistsAsync<T>(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default) where T : class
        {
            return await _context.Set<T>().AnyAsync(predicate, cancellationToken);
        }

        public int SaveChanges<T>() where T : class
        {
            return _context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync<T>(CancellationToken cancellationToken = default) where T : class
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}

