using Bidder.IdentityService.Application.Interfaces.Repos;
using Bidder.IdentityService.Infastructure.Context;
using Bidder.IdentityService.Infastructure.Repos;
using Bidder.Infastructure.Common.Uof;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Bidder.IdentityService.Infastructure.Uof
{
    public class UnitOfWork : BaseUnitOfWork<UserDbContext>, IUnitOfWork
    { 
         
        public UnitOfWork(UserDbContext context, ILogger<BaseUnitOfWork<UserDbContext>> _logger, IMediator mediator) : base(context, _logger, mediator)
        {
        }

        private IUserRepository userRepository;
        public IUserRepository UserRepository
        {
            get
            {
                userRepository = new UserRepository(this._dbContext);
                return userRepository;
            }
        }
    }
}
