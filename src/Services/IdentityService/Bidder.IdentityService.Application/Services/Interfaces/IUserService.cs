using Bidder.IdentityService.Domain.Entities;

namespace Bidder.IdentityService.Application.Services.Interfaces
{
    public interface IUserService
    {

        public Task<bool> IsEmailUnique(string email);

        public Task<Users> GetUserWithEmail(string email);

        public Task<Users> GetUserWithId(Guid id);

        public ValueTask<int> SaveUser(Users user, CancellationToken token);
    }
}
