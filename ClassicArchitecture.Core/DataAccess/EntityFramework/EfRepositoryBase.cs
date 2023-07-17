using ClassicArchitecture.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ClassicArchitecture.Core.DataAccess.EntityFramework
{
    public class EfRepositoryBase<TEntity, TContext> : IAsyncRepository<TEntity>, IRepository<TEntity>
        where TEntity : Entity
        where TContext : DbContext
    {

        protected TContext Context { get; }

        public EfRepositoryBase(TContext context)
        {
            Context = context;
        }

        public IQueryable<TEntity> Query()
        {
            return Context.Set<TEntity>();
        }

        public TEntity Add(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Added;
            Context.SaveChanges();
            return entity;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Added;
            await Context.SaveChangesAsync(true);
            return entity;
        }

        public TEntity Update(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            Context.SaveChanges();
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            var existingEntity = await Context.Set<TEntity>().FindAsync(entity.Id);
            if(existingEntity != null)
            {
                Context.Entry(existingEntity).CurrentValues.SetValues(entity);
                await Context.SaveChangesAsync(true);
                return existingEntity;
            }
            Context.Entry(entity).State = EntityState.Modified;
            await Context.SaveChangesAsync(true);
            return entity;
        }

        TEntity IRepository<TEntity>.Delete(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Deleted;
            Context.SaveChanges(true);
            return entity;
        }

        public async Task<TEntity> DeleteAsync(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Deleted;
            await Context.SaveChangesAsync(true);
            return entity;
        }

       

        public TEntity? Get(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, bool enableTracking = true)
        {
            IQueryable<TEntity> queryable = Query().AsQueryable();
            if (!enableTracking) queryable = queryable.AsNoTracking();
            if (include != null) queryable = include(queryable);
            return queryable.FirstOrDefault(predicate);
        }

        public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate, 
                                        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, 
                                        object>>? include = null, bool enableTracking = true, 
                                        CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> queryable = Query().AsQueryable();
            if (!enableTracking) queryable = queryable.AsNoTracking();
            if (include != null) queryable = include(queryable);
            return await queryable.FirstOrDefaultAsync(predicate, cancellationToken);
        }

        public List<TEntity>? GetAll(Expression<Func<TEntity, bool>>? predicate = null, 
                                     Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, 
                                     Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, 
                                     bool enableTracking = true)
        {
            IQueryable<TEntity> queryable = Query().AsQueryable();
            if (!enableTracking) queryable = queryable.AsNoTracking();
            if (include != null) queryable = include(queryable);
            if (predicate != null) queryable = queryable.Where(predicate);
            return orderBy != null ? orderBy(queryable).ToList() : queryable.ToList();
        }

        public async Task<List<TEntity>?> GetAllAsync(Expression<Func<TEntity, bool>>? predicate = null, 
                                                Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, 
                                                Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, 
                                                bool enableTracking = true, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> queryable = Query().AsQueryable();
            if (!enableTracking) queryable = queryable.AsNoTracking();
            if (include != null) queryable = include(queryable);
            if (predicate != null) queryable = queryable.Where(predicate);
            return await (orderBy != null ? orderBy(queryable).ToListAsync(cancellationToken) : queryable.ToListAsync(cancellationToken));
        }

       

        public async Task<IList<TEntity>> AddRangeAsync(IList<TEntity> entities)
        {
            await Context.Set<TEntity>().AddRangeAsync(entities);
            await Context.SaveChangesAsync(true);
            return entities;
        }



        IList<TEntity> IRepository<TEntity>.AddRange(IList<TEntity> entities)
        {
            Context.Set<TEntity>().AddRangeAsync(entities);
            Context.SaveChangesAsync(true);
            return entities;
        }

       
    }
}
