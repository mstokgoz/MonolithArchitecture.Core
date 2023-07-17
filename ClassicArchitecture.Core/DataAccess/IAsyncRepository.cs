using ClassicArchitecture.Core.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ClassicArchitecture.Core.DataAccess
{
    public interface IAsyncRepository<TEntity> where TEntity : Entity
    {

        IQueryable<TEntity> Query();

        Task<TEntity> AddAsync(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);

        Task<TEntity> DeleteAsync(TEntity entity);

        Task<IList<TEntity>> AddRangeAsync(IList<TEntity> entities);

        Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate,
                                Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
                                bool enableTracking = true, CancellationToken cancellationToken = default);

        Task<List<TEntity>?> GetAllAsync(Expression<Func<TEntity, bool>>? predicate = null,
                                         Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                         Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
                                         bool enableTracking = true, CancellationToken cancellationToken = default);
            
    }
}
