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
        public UnitOfWork(UserDbContext context, IMediator mediator)
        {
            this._userDbContext = context;
            this.mediator = mediator;
            currentTransaction = _userDbContext.Database.BeginTransaction();
        }
        public async void Dispose()
        {
            await _userDbContext.DisposeAsync();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellation = default)
        {
            await mediator.DispatchDomainEventsAsync(_userDbContext);
            int hasChanges = await _userDbContext.SaveChangesAsync();
            await currentTransaction.CommitAsync(cancellation);
            return hasChanges;
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



        private IUserRepository userRepository;
        public IUserRepository UserRepository
        {
            get
            {
                userRepository = new UserRepository(_userDbContext);
                return userRepository;
            }
        }
    }
}
