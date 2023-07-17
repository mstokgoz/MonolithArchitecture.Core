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
    public interface IRepository<TEntity> where TEntity : Entity
    {

        IQueryable<TEntity> Query();

        TEntity Add(TEntity entity);

        TEntity Update(TEntity entity);

        TEntity Delete(TEntity entity);

        IList<TEntity> AddRange(IList<TEntity> entities);

        TEntity? Get(Expression<Func<TEntity, bool>> predicate,
                                Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
                                bool enableTracking = true);

        List<TEntity>? GetAll(Expression<Func<TEntity, bool>>? predicate = null,
                                         Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                         Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
                                         bool enableTracking = true);
    }
}
