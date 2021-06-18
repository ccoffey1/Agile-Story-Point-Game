using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppServiceDemo.Data.Repository.Abstraction
{
    public class BaseRepository<TEntity, TContext> : IRepository<TEntity>
        where TEntity : class
        where TContext : DbContext
    {
        internal readonly TContext _context;
        internal readonly ILogger _logger;

        public BaseRepository(
            TContext context,
            ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> DeleteAsync(int id)
        {
            var entity = await _context.Set<TEntity>().FindAsync(id);
            if (entity == null)
            {
                return entity;
            }

            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<TEntity> GetAsync(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task ExecuteInTransaction(Func<Task> action)
        {
            // execution strategy required to fix error around transactions + exec strategy we're using at startup
            var executionStrategy = _context.Database.CreateExecutionStrategy();

            await executionStrategy.Execute(async () =>
            {
                using (var dbContextTransaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        await action.Invoke();
                        dbContextTransaction.Commit();
                    }
                    catch (Exception e)
                    {
                        _logger.LogError("An error occured committing a transaction to multiple tables.", e);
                        dbContextTransaction.Rollback();
                        throw;
                    }
                }
            });            
        }
    }
}
