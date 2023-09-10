using Bidder.BidService.Application.Interfaces.Repos;
using Bidder.BidService.Infastructure.Context;
using Bidder.BidService.Infastructure.Extensions;
using Bidder.BidService.Infastructure.Repos;
using MediatR;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace Bidder.BidService.Infastructure.Uof
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BidDbContext _bidDbContext;
        private readonly ILogger<UnitOfWork> logger;
        private readonly IMediator mediator;
        private IDbContextTransaction currentTransaction;
        public IDbContextTransaction GetCurrentTransaction() => currentTransaction;
        public bool HasActiveTransaction => currentTransaction != null;
        public UnitOfWork(BidDbContext context, ILogger<UnitOfWork> _logger, IMediator mediator)
        {
            this._bidDbContext = context;
            this.mediator = mediator;
            logger = _logger;
            currentTransaction = _bidDbContext.Database.BeginTransaction();
        }
        public async void Dispose()
        {
            logger.LogInformation($"Disposing UnitOfWork.{nameof(_bidDbContext)}");
            await _bidDbContext.DisposeAsync();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellation = default)   
        {
            try
            {
                logger.LogInformation($"Saving Changes. Transaction Id: {this.currentTransaction.TransactionId}");
                await mediator.DispatchDomainEventsAsync(_bidDbContext);
                int hasChanges = await _bidDbContext.SaveChangesAsync();
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



        private IBidRepository _bidRepository;
        public IBidRepository BidRepository
        {
            get
            {
                _bidRepository = new BidRepository(_bidDbContext);
                return _bidRepository;
            }
        }
         
    }
}
