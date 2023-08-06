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
            RuleFor(x => x.Email).NotEmpty().EmailAddress().MinimumLength(5).MaximumLength(200);
            RuleFor(x => x.Password).NotEmpty();
            RuleFor(x => x.Name).NotEmpty().MinimumLength(3).MaximumLength(100);
            RuleFor(x => x.Surname).NotEmpty().MinimumLength(3).MaximumLength(100);
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
