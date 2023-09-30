using Bidder.Domain.Common.Entity;

namespace Bidder.BidService.Domain.Entities
{
    public class BidRoomUser : BaseEntity
    {
        private Guid _userId;
        private string _userName;
        private long _bidRoomId;
        private DateTime _joinedDate;
        private DateTime? _exitDate;


        private BidRoom _bidRoom;

        public Guid UserId { get => _userId; set => _userId = value; }
        public string UserName { get => _userName; set => _userName = value; }
        public long BidRoomId { get => _bidRoomId; set => _bidRoomId = value; }
        public DateTime JoinedDate { get => _joinedDate; set => _joinedDate = value; }
        public DateTime? ExitDate { get => _exitDate; set => _exitDate = value; } 
        public BidRoom BidRoom { get => _bidRoom; set => _bidRoom = value; }
    }
}
