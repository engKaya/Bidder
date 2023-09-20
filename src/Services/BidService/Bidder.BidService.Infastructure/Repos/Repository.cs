﻿using Bidder.BidService.Infastructure.Context;
using Bidder.Domain.Common.Entity;
using Bidder.Domain.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bidder.BidService.Infastructure.Repos
{
    //Bu şekilde her repo için ayrı ayrı uof yazmak yerine tek bir uof yazıp her repo için ayrı ayrı dbcontextleri register edip kullanmak daha mantıklı
    //TODO : Bu şekilde bir yapı kurulursa ayrı ayrı bu classa gerek kalmayacak
    public class Repository<T> : IRepository<T> where T : BaseEntity 
    {
        protected readonly BidDbContext context;

        public Repository(BidDbContext context)
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
        public async Task<T> FindFirst(Expression<Func<T, bool>> predicate, Func<IQueryable, IOrderedQueryable<T>>? orderBy = null, params Expression<Func<T, object>>[] includes)
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
            return await query.FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<T>> GetAll() => await context.Set<T>().ToListAsync();

        /// <summary>
        /// Verilen Koşullara Göre IEnumarable Dönen Metod
        /// </summary>
        /// <param name="predicate">Koşul parametresi</param>
        /// <param name="orderBy">Eğer istenir ise order by parametresi</param>
        /// <param name="includes">Eğer gerekirse Configuration üzerinde maplenmiş ilişkili objeleri de eklemesini sağlayan parametre</param>
        /// <returns>IEnumerable<T></returns>
        public async Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate, Func<IQueryable, IOrderedQueryable<T>>? orderBy = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = context.Set<T>().Where(predicate);

            if (includes is not null) query = includes.Aggregate(query, (current, include) => current.Include(include));

            if (orderBy is not null) query = orderBy(query);

            return await query.ToListAsync();
        }

        public T Update(T entity)
        {
            context.Set<T>().Update(entity);
            return entity;
        }

        public IEnumerable<T> UpdateRange(IEnumerable<T> entities)
        {
            context.Set<T>().UpdateRange(entities);
            return entities;
        }
    }
}
