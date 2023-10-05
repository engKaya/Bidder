namespace Bidder.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Guid GetUserId();
        string GetUserName();
        string GetUserEmail();
    }
}
