namespace Bidder.BidService.Application.Interfaces.Repos
{
    public interface IIdentityService
    {
        Guid GetUserId();
        string GetUserName();
        string GetUserEmail();
    }
}
