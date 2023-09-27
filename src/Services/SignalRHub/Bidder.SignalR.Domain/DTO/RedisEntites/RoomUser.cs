namespace Bidder.SignalR.Domain.DTO.RedisEntites
{
    public class RoomUser
    {
        public Guid RoomId { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string ConnectionId { get; set; }
        public DateTime JoinDate { get; set; }

        public RoomUser(Guid roomId, Guid userId, string userName, string connectionId)
        {
            RoomId = roomId;
            UserId = userId;
            UserName = userName;
            ConnectionId = connectionId;
        } 
    }
}
