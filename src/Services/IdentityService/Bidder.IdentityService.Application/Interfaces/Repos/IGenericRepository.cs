using Bidder.IdentityService.Domain.Entities;
using Bidder.IdentityService.Domain.Interfaces;
using System.Linq.Expressions;

namespace Bidder.IdentityService.Application.Interfaces.Repos
{
    public interface IGenericRepository<T> : IRepository<T> where T : BaseEntity
    {
        Task<List<T>> GetAllAsync();
        Task<List<T>> Get(Expression<Func<T, bool>> predicate = null, Func<IQueryable, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] includes);
        Task<List<T>> Get(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties);
        Task<T> GetById(Guid id);
        Task<T> GetByIdAsync(Guid id, params Expression<Func<T, object>>[] includeProperties);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties);
        Task<T> AddAsync(T entity);
        T Update(T entity);
    }
}
