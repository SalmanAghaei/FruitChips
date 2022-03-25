using Core.Contracts.Data.Commands;
using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistance.SqlData.Context;
using System.Linq.Expressions;

namespace Persistance.SqlData.Commands
{
    public class CommandRepository<TEntity, Tkey, TDbContext> : ICommandRepository<TEntity,Tkey>, IUnitOfWork
         where TEntity : BaseEntity<Tkey>
        where TDbContext : CommandBaseContext
    {
        protected readonly TDbContext _dbContext;

        public CommandRepository(TDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Delete(Tkey id)
        {
            _dbContext.Set<TEntity>();
            var entity = _dbContext.Set<TEntity>().Find(id);
            _dbContext.Set<TEntity>().Remove(entity);
        }


        public void Delete(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }



        public void DeleteRange(Tkey[] ids)
        {
            _dbContext.RemoveRange(ids);
        }
    

        public TEntity Get(Tkey id)
        {
            return _dbContext.Set<TEntity>().Find(id);
        }
        public async Task<TEntity> GetAsync(Tkey id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }

        public void Insert(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
        }

        public async Task InsertAsync(TEntity entity)
        {
           await _dbContext.Set<TEntity>().AddAsync(entity);
        }

        public bool Exists(Expression<Func<TEntity, bool>> expression)
        {
            return _dbContext.Set<TEntity>().Any(expression);
        }


        public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await _dbContext.Set<TEntity>().AnyAsync(expression);
        }

        public int Commit()
        {
            return _dbContext.SaveChanges();
        }

        public Task<int> CommitAsync()
        {
            return _dbContext.SaveChangesAsync();
        }

        public void DeleteGraph(long id)
        {
            throw new NotImplementedException();
        }

        public TEntity GetGraph(long id)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> GetGraphAsync(long id)
        {
            throw new NotImplementedException();
        }

        public void InsertRange(IEnumerable<TEntity> entities)
        {
            _dbContext.Set<TEntity>().AddRange(entities);
        }

        public async Task InsertRangeAsync(IEnumerable<TEntity> entities)
        {
            await _dbContext.Set<TEntity>().AddRangeAsync(entities);
        }

        public void BeginTransaction()
        {
            _dbContext.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _dbContext.CommitTransaction();
        }

        public void RollbackTransaction()
        {
            _dbContext.RollbackTransaction();
        }

        public void DeleteGraph(Tkey id)
        {
            throw new NotImplementedException();
        }

    

        public TEntity GetGraph(Tkey id)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> GetGraphAsync(Tkey id)
        {
            throw new NotImplementedException();
        }
    }
}
