using Bidder.IdentityService.Application.Interfaces.Repos;
using Bidder.IdentityService.Domain.Entities;
using Bidder.IdentityService.Domain.Interfaces;
using Bidder.IdentityService.Infastructure.Context;
using Microsoft.EntityFrameworkCore; 
using System.Linq.Expressions;

namespace Bidder.IdentityService.Infastructure.Repos
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly UserDbContext dbContext; 
        public IUnitOfWork UnitOfWork => dbContext;
        public GenericRepository(UserDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));   
        } 

        public async Task<T> AddAsync(T entity)
        {
            await dbContext.Set<T>().AddAsync(entity);  
            return entity;  
        }

        public virtual async Task<List<T>> Get(Expression<Func<T, bool>> predicate = null, Func<IQueryable, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> queryable = dbContext.Set<T>();
            foreach (Expression<Func<T, object>> include in includes)
            {
                queryable.Include(include);
            }

            if (predicate != null)
                queryable.Where(predicate);

            if (orderBy != null)
                queryable = orderBy(queryable);

            return await queryable.ToListAsync();
        }

        public virtual async Task<List<T>> Get(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties)
        {
            return await Get(predicate, null, includeProperties);
        }

        public virtual async Task<List<T>> GetAllAsync()
        {
            return await dbContext.Set<T>().ToListAsync();
        }

        public virtual async Task<T> GetById(Guid id)
        {
            return await dbContext.Set<T>().FindAsync(id);
        }

        public virtual async Task<T> GetByIdAsync(Guid id, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = dbContext.Set<T>();

            foreach (var x in includeProperties)
            {
                query.Include(x);
            }

            return await query.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async virtual Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = dbContext.Set<T>();

            foreach (var x in includeProperties)
            {
                query.Include(x);
            }

            return await query.Where(predicate).SingleOrDefaultAsync();
        }

        public T Update(T entity)
        {
            dbContext.Set<T>().Update(entity);
            return entity;
        }
    }
}
