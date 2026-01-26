using Leaguelane.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Leaguelane.Repository.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly LeaguelaneDbContext _context;

        public GenericRepository(LeaguelaneDbContext context)
        {
            _context = context;
        }

        // Synchronous query methods
        public virtual T GetById(object id)
        {
            return _context.Set<T>().Find(id);
        }

        public virtual IQueryable<T> GetAll()
        {
            return _context.Set<T>().AsQueryable();
        }

        public virtual IQueryable<T> FindAll(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }

        public virtual T FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().FirstOrDefault(predicate);
        }

        // Async query methods
        public virtual async Task<T> GetByIdAsync(object id, CancellationToken cancellationToken = default)
        {
            return await _context.Set<T>().FindAsync(id, cancellationToken);
        }

        public virtual Task<IQueryable<T>> GetAllAsync()
        {
            return Task.FromResult(_context.Set<T>().AsQueryable());
        }

        public virtual Task<IQueryable<T>> FindAllAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_context.Set<T>().Where(predicate));
        }

        public virtual async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(predicate, cancellationToken);
        }

        // Synchronous CRUD operations
        public virtual T Add(T entity)
        {
            var entry = _context.Set<T>().Add(entity);
            return entry.Entity;
        }

        public virtual IEnumerable<T> AddRange(IEnumerable<T> entities)
        {
            var entityList = entities.ToList();
            _context.Set<T>().AddRange(entityList);
            return entityList;
        }

        public virtual void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public virtual void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public virtual void RemoveRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

        // Async CRUD operations
        public virtual async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            var entry = await _context.Set<T>().AddAsync(entity, cancellationToken);
            return entry.Entity;
        }

        public virtual async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            var entityList = entities.ToList();
            await _context.Set<T>().AddRangeAsync(entityList, cancellationToken);
            return entityList;
        }

        public virtual Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            return Task.CompletedTask;
        }

        public virtual Task RemoveAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }

        public virtual Task RemoveRangeAsync(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
            return Task.CompletedTask;
        }

        // Count
        public virtual int Count(Expression<Func<T, bool>> predicate = null)
        {
            return predicate == null ? _context.Set<T>().Count() : _context.Set<T>().Count(predicate);
        }

        public virtual async Task<int> CountAsync(Expression<Func<T, bool>> predicate = null, CancellationToken cancellationToken = default)
        {
            return predicate == null ? await _context.Set<T>().CountAsync(cancellationToken) : await _context.Set<T>().CountAsync(predicate, cancellationToken);
        }

        // Exists
        public virtual bool Exists(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Any(predicate);
        }

        public virtual async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _context.Set<T>().AnyAsync(predicate, cancellationToken);
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
