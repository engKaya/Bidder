using Bidder.Application.Common.Interfaces;
using Bidder.Infastructure.Common.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace Bidder.Infastructure.Common.Uof
{
    public class BaseUnitOfWork<T> : IBaseUnitOfWork where T : DbContext
    {
        protected readonly T _dbContext;
        private readonly ILogger<BaseUnitOfWork<T>> logger;
        private readonly IMediator mediator;
        private IDbContextTransaction currentTransaction;
        public IDbContextTransaction GetCurrentTransaction() => currentTransaction;
        public bool HasActiveTransaction => currentTransaction != null;
        public BaseUnitOfWork(T context, ILogger<BaseUnitOfWork<T>> _logger, IMediator mediator)
        {
            this._dbContext = context;
            this.mediator = mediator;
            logger = _logger;
            currentTransaction = _dbContext.Database.BeginTransaction();
        }
        public async void Dispose()
        {
            logger.LogInformation($"Disposing UnitOfWork.{nameof(_dbContext)}");
            await _dbContext.DisposeAsync();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellation = default)
        {
            try
            {
                logger.LogInformation($"Saving Changes. Transaction Id: {this.currentTransaction.TransactionId}");
                await mediator.DispatchDomainEventsAsync(_dbContext);
                int hasChanges = await _dbContext.SaveChangesAsync();
                await currentTransaction.CommitAsync(cancellation);
                logger.LogInformation($"Changes Commited. Transaction Id: {this.currentTransaction.TransactionId}, {hasChanges} changes committed!"); 
                return hasChanges;
            }
            catch (Exception ex)
            {
                logger.LogCritical($"Error occured at commiting changes. Transaction Id: {this.currentTransaction.TransactionId}.\n Ex: {ex}\n Stack Trace: {ex.StackTrace} ");
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
    }
}
