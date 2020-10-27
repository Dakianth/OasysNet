using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OasysNet.Data.Contexts;
using OasysNet.Domain.Core.Data;
using OasysNet.Domain.Core.Models;
using OasysNet.Domain.Interfaces.Data;

namespace OasysNet.Data.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity<TEntity>
    {
        protected readonly ApplicationContext Context;
        protected readonly DbSet<TEntity> DbSet;
        private bool _disposed;

        protected Repository(ApplicationContext context)
        {
            Context = context;
            DbSet = Context.Set<TEntity>();
            //Db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        IUnitOfWork IRepository<TEntity>.UnitOfWork => Context;

        public async ValueTask<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await Include().SingleOrDefaultAsync(c => c.Id == id, cancellationToken);
        }

        public async ValueTask<List<TEntity>> GetAsync(CancellationToken cancellationToken = default)
        {
            return await Include().ToListAsync(cancellationToken);
        }

        public async ValueTask<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await Include().Where(predicate).ToListAsync(cancellationToken);
        }

        public ValueTask<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            var entityEntry = DbSet.Add(entity);
            return new ValueTask<TEntity>(entityEntry.Entity);
        }

        public ValueTask<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            var entityEntry = DbSet.Update(entity);
            return new ValueTask<TEntity>(entityEntry.Entity);
        }

        public ValueTask DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            DbSet.Remove(entity);
            return new ValueTask();
        }

        public ValueTask DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = DbSet.Find(id);
            DbSet.Remove(entity);
            return new ValueTask();
        }

        protected virtual IQueryable<TEntity> Include()
        {
            return DbSet;
        }

        protected virtual Task<int> SaveChanges()
        {
            return Context.SaveChangesAsync();
        }

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
                Context.Dispose();

            _disposed = true;
        }

        #endregion IDisposable
    }
}