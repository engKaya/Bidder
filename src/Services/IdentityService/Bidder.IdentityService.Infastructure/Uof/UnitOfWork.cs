using Bidder.IdentityService.Application.Interfaces.Repos;
using Bidder.IdentityService.Infastructure.Context;
using Bidder.IdentityService.Infastructure.Extensions;
using Bidder.IdentityService.Infastructure.Repos;
using MediatR;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace Bidder.IdentityService.Infastructure.Uof
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly UserDbContext _userDbContext;
        private readonly ILogger<UnitOfWork> logger;
        private readonly IMediator mediator;
        private IDbContextTransaction currentTransaction;
        public IDbContextTransaction GetCurrentTransaction() => currentTransaction;
        public bool HasActiveTransaction => currentTransaction != null;
        public UnitOfWork(UserDbContext context, ILogger<UnitOfWork> _logger, IMediator mediator)
        {
            this._userDbContext = context;
            this.mediator = mediator;
            logger = _logger;
            currentTransaction = _userDbContext.Database.BeginTransaction();
        }
        public async void Dispose()
        {
            logger.LogInformation($"Disposing UnitOfWork.{nameof(_userDbContext)}");
            await _userDbContext.DisposeAsync();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellation = default)
        {
            try
            {
                logger.LogInformation($"Saving Changes. Transaction Id: {this.currentTransaction.TransactionId}"); 
                await mediator.DispatchDomainEventsAsync(_userDbContext);
                int hasChanges = await _userDbContext.SaveChangesAsync();
                await currentTransaction.CommitAsync(cancellation);
                logger.LogInformation($"Changes Commited. Transaction Id: {this.currentTransaction.TransactionId}, {hasChanges} changes committed!");
                return hasChanges;
            }
            catch (Exception ex)
            {
                logger.LogCritical($"Error occured at commiting changes. Transaction Id: {this.currentTransaction.TransactionId}.\n Ex: {ex.ToString()}\n Stack Trace: {ex.StackTrace} ");
                this.RollbackTransaction();
                throw;
            }
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
