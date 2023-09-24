using Bidder.BidService.Application.Interfaces.Repos;
using Bidder.BidService.Infastructure.Context;
using Bidder.BidService.Infastructure.Repos;
using Bidder.Infastructure.Common.Uof;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Bidder.BidService.Infastructure.Uof
{
    public class UnitOfWork : BaseUnitOfWork<BidDbContext>, IUnitOfWork
    {

        public UnitOfWork(BidDbContext context, ILogger<BaseUnitOfWork<BidDbContext>> _logger, IMediator mediator) : base(context, _logger, mediator)
        {
        } 

        private IBidRepository _bidRepository;
        public IBidRepository BidRepository
        {
            get
            {
                _bidRepository = new BidRepository(this._dbContext);
                return _bidRepository;
            }
        }
        private IBidRoomRepository _bidRoomRepository;
        public IBidRoomRepository BidRoomRepository
        {
            get
            {
                _bidRoomRepository = new BidRoomRepository(this._dbContext);
                return _bidRoomRepository;
            }
        }
    }
}
