using Bidder.IdentityService.Domain.Entities;
using Bidder.IdentityService.Domain.Interfaces;
using Bidder.IdentityService.Infastructure.EntityConfigrations;
using Bidder.IdentityService.Infastructure.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Bidder.IdentityService.Infastructure.Context
{
    public class UserDbContext : DbContext, IUnitOfWork
    {
        private readonly IMediator mediator;
        private IDbContextTransaction _currentTransaction;
        public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;
        public bool HasActiveTransaction => _currentTransaction != null;
        public const string DEFAULT_SCHEMA = "BIDDER_USER";
         
        public UserDbContext(DbContextOptions<UserDbContext> options, IMediator mediator) : base(options)
        {
            this.mediator = mediator;
        }

        public DbSet<Users> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserDbConfiguration());
        } 
        public  async Task<bool> SaveEntitiesAsync(CancellationToken cancellation = default)
        {
            await mediator.DispatchDomainEventsAsync(this); 
            await this.SaveChangesAsync(cancellation);
            
            return true;
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }
    }
}
