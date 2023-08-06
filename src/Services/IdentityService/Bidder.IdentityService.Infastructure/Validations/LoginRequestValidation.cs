using Bidder.IdentityService.Domain.DTOs.User.Request;
using FluentValidation;

namespace Bidder.IdentityService.Infastructure.Validations
{
    public class LoginRequestValidation : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidation()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress().MinimumLength(5).MaximumLength(200);
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
