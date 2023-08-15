using Bidder.IdentityService.Domain.DTOs;
using Bidder.IdentityService.Domain.DTOs.User.Request;
using Bidder.IdentityService.Domain.DTOs.User.Responses;
using MediatR;

namespace Bidder.IdentityService.Application.Features.Queries.User.Login
{
    public class LoginQuery : IRequest<ResponseMessage<LoginResponse>>
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public LoginQuery()
        {
        }
        public LoginQuery(LoginRequest request)
        {
            Email = request.Email;
            Password = request.Password;
        }
    }
}
