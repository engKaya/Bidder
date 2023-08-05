using Bidder.IdentityService.Domain.Entities;
using Bidder.IdentityService.Domain.Interfaces;
using Bidder.IdentityService.Infastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bidder.IdentityService.Infastructure.Repos
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly UserDbContext context;

        public Repository(UserDbContext context)
        {
            this.context = context;
        }

        public T Add(T entity)
        {
            context.Set<T>().Add(entity);
            return entity;
        }

        public IEnumerable<T> AddRange(IEnumerable<T> entities)
        {
            context.Set<T>().AddRange(entities);
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
