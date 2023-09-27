using System.Net;

namespace Bidder.SignalR.Domain.DTO.Responses.Join
{
    public class JoinResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; } 
        public string ConnectionId { get; set; }

        public JoinResponse()
        {

        }
        public JoinResponse(HttpStatusCode statusCode, string message, string connectionId)
        {
            StatusCode = statusCode;
            Message = message;
            ConnectionId = connectionId;
        }
    }
}
