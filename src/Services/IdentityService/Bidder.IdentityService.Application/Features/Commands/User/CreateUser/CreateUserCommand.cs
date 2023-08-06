using Bidder.IdentityService.Domain.DTOs;
using Bidder.IdentityService.Domain.DTOs.User.Request;
using MediatR;

namespace Bidder.IdentityService.Application.Features.Commands.User.CreateUser
{
    public class CreateUserCommand : IRequest<ResponseMessageNoContent>
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;

        public CreateUserCommand()
        {

        }

        public CreateUserCommand(CreateUserRequest req)
        {
            this.Email = req.Email;
            this.Password = req.Password;
            this.Name = req.Name;
            this.Surname = req.Surname;
        }
    }
}
