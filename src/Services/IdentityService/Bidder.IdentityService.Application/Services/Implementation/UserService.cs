using Bidder.IdentityService.Application.Interfaces.Repos;
using Bidder.IdentityService.Application.Services.Interfaces;
using Bidder.IdentityService.Domain.Entities;

namespace Bidder.IdentityService.Application.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> IsEmailUnique(string email)
        {
            var user = await _unitOfWork.UserRepository.FindFirst(x => x.Email == email);
            return user is null;
        }

        public async Task<Users> GetUserWithEmail(string email)
        {
            var user = await _unitOfWork.UserRepository.FindFirst(x => x.Email == email);
            return user;
        }

        public async Task<Users> GetUserWithId(Guid id)
        {
            var user = await _unitOfWork.UserRepository.FindFirst(x => x.Id == id);
            return user;
        }

        public async ValueTask<int> SaveUser(Users user, CancellationToken token)
        {
            await _unitOfWork.UserRepository.Add(user, token);
            return await _unitOfWork.SaveChangesAsync();
        }
    }
}
