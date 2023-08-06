using Bidder.IdentityService.Domain.Entities;
using System.Linq.Expressions;

namespace Bidder.IdentityService.Domain.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> FindFirst(Expression<Func<T, bool>> predicate, Func<IQueryable, IOrderedQueryable<T>>? orderBy = null, params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> GetAll();

        /// <summary>
        /// Verilen Koşullara Göre IEnumarable Dönen Metod
        /// </summary>
        /// <param name="predicate">Koşul parametresi</param>
        /// <param name="orderBy">Eğer istenir ise order by parametresi</param>
        /// <param name="includes">Eğer gerekirse Configuration üzerinde maplenmiş ilişkili objeleri de eklemesini sağlayan parametre</param>
        /// <returns>IEnumerable<T></returns>
        Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate, Func<IQueryable, IOrderedQueryable<T>>? orderBy = null, params Expression<Func<T, object>>[] includes);
        Task<T> Add(T entity);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);
        T Update(T entity);
        IEnumerable<T> UpdateRange(IEnumerable<T> entities);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);
    }
}
