using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using OasysNet.Domain.Core.Data;

namespace OasysNet.Domain.Interfaces.Data
{
    public interface IRepository<TEntity> : IDisposable
    {
        IUnitOfWork UnitOfWork { get; }

        ValueTask<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        ValueTask<List<TEntity>> GetAsync(CancellationToken cancellationToken = default);

        ValueTask<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        ValueTask<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default);

        ValueTask<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

        ValueTask DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);

        ValueTask DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}