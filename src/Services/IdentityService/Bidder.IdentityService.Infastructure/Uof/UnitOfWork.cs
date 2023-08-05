using Bidder.IdentityService.Application.Interfaces.Repos;
using Bidder.IdentityService.Infastructure.Context;
using Bidder.IdentityService.Infastructure.Extensions;
using Bidder.IdentityService.Infastructure.Repos;
using MediatR;
using Microsoft.EntityFrameworkCore.Storage;

namespace Bidder.IdentityService.Infastructure.Uof
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly UserDbContext _userDbContext;
        private readonly IMediator mediator;
        private IDbContextTransaction currentTransaction;
        public IDbContextTransaction GetCurrentTransaction() => currentTransaction;
        public bool HasActiveTransaction => currentTransaction != null;

        private IUserRepository userRepository;
        public IUserRepository UserRepository => userRepository  ?? new UserRepository(_userDbContext);
        public UnitOfWork(UserDbContext context, IMediator mediator)
        {
            this._userDbContext= context;
            this.mediator = mediator;
            currentTransaction = _userDbContext.Database.BeginTransaction();
        }
        public void Dispose()
        { 

        }

        public async Task<bool> SaveChangesAsync(CancellationToken cancellation = default)
        {
            await mediator.DispatchDomainEventsAsync(_userDbContext);
            await _userDbContext.SaveChangesAsync(cancellation);

            return true;
        }

        public void RollbackTransaction()
        {
            try
            {
                currentTransaction?.Rollback();
            }
            finally
            {
                if (currentTransaction != null)
                {
                    currentTransaction.Dispose();
                    currentTransaction = default;
                }
            }
        }
    }
}
