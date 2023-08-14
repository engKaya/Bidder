namespace Bidder.Notification.Application.Response
{
    public class EmailResponse
    {

        public DateTime Date { get; set; } = DateTime.UtcNow;
        public bool IsSuccess { get; set; }
        public string Message { get; set; } 
        public EmailResponse(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }
    }
}
