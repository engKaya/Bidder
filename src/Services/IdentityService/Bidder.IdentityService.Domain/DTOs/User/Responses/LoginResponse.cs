namespace Bidder.IdentityService.Domain.DTOs.User.Responses
{
    public class LoginResponse
    {
        public string Token { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty; 
        public DateTime TokenLife => DateTime.Now.AddHours(2);
    }
}
