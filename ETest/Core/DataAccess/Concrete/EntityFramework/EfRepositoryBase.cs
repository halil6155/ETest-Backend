using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.DataAccess.Abstract;
using Core.Entities.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Core.DataAccess.Concrete.EntityFramework
{
    public class EfRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity> where TEntity : class, IEntity, new() where TContext : DbContext
    {
        protected readonly TContext Context;


        public EfRepositoryBase(TContext context)
        {
            Context = context;
        }
        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] includes)
        {
            var result = Context.Set<TEntity>().AsQueryable();
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    result = result.Include(include);
                }
            }
            return await result.FirstOrDefaultAsync(filter);
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] includes)
        {
            var result = Context.Set<TEntity>().AsQueryable();
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    result = result.Include(include);
                }
            }
            return result.FirstOrDefault(filter);
        }

        public IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] includes)
        {
            var result = Context.Set<TEntity>().AsQueryable();
            if (filter != null)
                result = result.Where(filter);
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    result = result.Include(include);
                }
            }
            return result.ToList();
        }

        public async Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] includes)
        {
            var result = Context.Set<TEntity>().AsQueryable();
            if (filter != null)
                result = result.Where(filter);
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    result = result.Include(include);
                }
            }
            return await result.ToListAsync();
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await Context.AddAsync(entity);
            return entity;
        }

        public TEntity Add(TEntity entity)
        {
            Context.Add(entity);
            return entity;
        }

        public TEntity Delete(TEntity entity)
        {
            Context.Remove(entity);
            return entity;
        }

        public TEntity Update(TEntity entity)
        {
            foreach (var trackEntity in Context.ChangeTracker.Entries())
                trackEntity.State = EntityState.Detached;
            Context.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public IQueryable<TEntity> GetQueryable()
        {
            return Context.Set<TEntity>().AsQueryable();
        }

        public IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities)
        {
            var addedEntities = entities.ToList();
            Context.AddRangeAsync(addedEntities);
            return addedEntities;
        }
        public async Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities)
        {
            var addedEntities = entities.ToList();
            await Context.AddRangeAsync(addedEntities);
            return addedEntities;
        }
        public IEnumerable<TEntity> RemoveRange(IEnumerable<TEntity> entities)
        {
            var deletedEntities = entities.ToList();
            Context.RemoveRange(deletedEntities);
            return deletedEntities;

        }

        public int SaveChanges()
        {
            return Context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await Context.SaveChangesAsync();
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            return filter == null ? await Context.Set<TEntity>().CountAsync() : await Context.Set<TEntity>().CountAsync(filter);

        }

        public int Count(Expression<Func<TEntity, bool>> filter = null)
        {
            return filter == null ? Context.Set<TEntity>().Count() : Context.Set<TEntity>().Count(filter);
        }

        public bool Any(Expression<Func<TEntity, bool>> filter)
        {
            return Context.Set<TEntity>().Any();
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await Context.Set<TEntity>().AnyAsync(filter);
        }
    }
}