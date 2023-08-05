using Bidder.IdentityService.Application.Interfaces.Repos;
using Bidder.IdentityService.Domain.DTOs.User.Request;
using FluentValidation;
namespace Bidder.IdentityService.Infastructure.Validations
{
    public class CreateUserRequestValidation : AbstractValidator<CreateUserRequest>
    {
        private readonly IUnitOfWork uof;

        public  CreateUserRequestValidation(IUnitOfWork _uof)
        {
            uof= _uof;
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty();
            RuleFor(x=>x.Email).Custom((email,context) =>
            {
                var user = uof.UserRepository.FindFirst(x => x.Email == email).GetAwaiter().GetResult();
                 
                if (user is not null) 
                {
                    context.AddFailure("Email", "Email Already Exists");
                }
            });
        }
    }
}
