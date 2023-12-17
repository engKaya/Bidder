using Bidder.BidService.Application.Interfaces.Repos;
using Bidder.BidService.Application.Interfaces.Services;
using Bidder.BidService.Domain.Entities;

namespace Bidder.BidService.Infastructure.Services
{
    public class BidRoomUserService : IBidRoomUserService
    {
        private readonly IBidRoomUserRepository _bidRoomRepository;

        public BidRoomUserService(IBidRoomUserRepository bidRoomRepository)
        {
            _bidRoomRepository = bidRoomRepository;
        }

        public async Task<IEnumerable<BidRoomUser>> GetBidRoomUsers(long roomId)
        {
            var result = await _bidRoomRepository.GetWhere(x=>x.BidRoomId == roomId);

            return result;
        }
    }
}
