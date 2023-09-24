namespace Bidder.BidService.Application.Interfaces.Services
{
    public interface IIdentityService
    {
        Guid GetUserId();
        string GetUserName();
        string GetUserEmail();
    }
}
