﻿using Bidder.Domain.Common.Entity;
using Bidder.Domain.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Bidder.Infastructure.Common.Repos
{
    public class BaseRepository<T> : IBaseRepository<T> where T : DBEntity
    {
        protected readonly DbContext context;

        public BaseRepository(DbContext context)
        {
            this.context = context;
        }

        public async Task<T> Add(T entity, CancellationToken cancellation)
        {
            await context.Set<T>().AddAsync(entity, cancellation);
            return entity;
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            await context.Set<T>().AddRangeAsync(entities);
            return entities;
        }

        public void Delete(T entity) => context.Set<T>().Remove(entity);

        public void DeleteRange(IEnumerable<T> entities) => context.Set<T>().RemoveRange(entities);

        /// <summary>
        /// Verilen Koşullara Göre Bulunan ilk kaydı Dönen Metod
        /// </summary>
        /// <param name="predicate">Koşul parametresi</param>
        /// <param name="orderBy">Eğer istenir ise order by parametresi</param>
        /// <param name="includes">Eğer gerekirse Configuration üzerinde maplenmiş ilişkili objeleri de eklemesini sağlayan parametre</param>
        /// <returns>IEnumerable<T></returns>
        public async Task<T> FindFirst(Expression<Func<T, bool>> predicate, Func<IQueryable, IOrderedQueryable<T>>? orderBy = null, CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] includes)
        {
            var query = context.Set<T>().Where(predicate);
            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            return await query.FirstOrDefaultAsync(cancellationToken);
        }
        public async Task<IEnumerable<T>> GetAll(CancellationToken cancellationToken) => await context.Set<T>().ToListAsync(cancellationToken);

        /// <summary>
        /// Verilen Koşullara Göre IEnumarable Dönen Metod
        /// </summary>
        /// <param name="predicate">Koşul parametresi</param>
        /// <param name="orderBy">Eğer istenir ise order by parametresi</param>
        /// <param name="includes">Eğer gerekirse Configuration üzerinde maplenmiş ilişkili objeleri de eklemesini sağlayan parametre</param>
        /// <returns>IEnumerable<T></returns>
        public async Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate, Func<IQueryable, IOrderedQueryable<T>>? orderBy = null, CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = context.Set<T>().Where(predicate);

            if (includes is not null) query = includes.Aggregate(query, (current, include) => current.Include(include));

            if (orderBy is not null) query = orderBy(query);

            return await query.ToListAsync(cancellationToken);
        }

        public T Update(T entity)
        {
            context.Set<T>().Update(entity);
            return entity;
        }

        public async Task<int> BulkUpdate(Expression<Func<T, bool>> predicate, Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>>  setProp,CancellationToken token)  => await context.Set<T>().Where(predicate).ExecuteUpdateAsync(setProp, token);  

        public IEnumerable<T> UpdateRange(IEnumerable<T> entities)
        {
            context.Set<T>().UpdateRange(entities);
            return entities;
        }
    }
}
