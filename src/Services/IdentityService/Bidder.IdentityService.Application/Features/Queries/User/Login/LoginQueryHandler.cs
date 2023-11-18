using Bidder.Domain.Common.BaseClassess;
using Bidder.IdentityService.Application.Services.Interfaces;
using Bidder.IdentityService.Domain.DTOs.User.Responses;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace Bidder.IdentityService.Application.Features.Queries.User.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, ResponseMessage<LoginResponse>>
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        public LoginQueryHandler(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        public async Task<ResponseMessage<LoginResponse>> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetUserWithEmail(request.Email);
            if (user is null || !user.VerifyPassword(request.Password)) return ResponseMessage<LoginResponse>.Fail("Password Or Email not correct", (int)HttpStatusCode.Unauthorized);
            var secretKey = _configuration["CustomSettings:Key"];

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.UserData, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Username),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creeds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expire = DateTime.Now.AddHours(2);
            var token = new JwtSecurityToken(claims: claims, expires: expire, signingCredentials: creeds, notBefore: DateTime.Now);
            var encodedToken = new JwtSecurityTokenHandler().WriteToken(token);

            return new ResponseMessage<LoginResponse>
            {
                Message = "Login Successfull",
                Data = new LoginResponse()
                {
                    Token = encodedToken,
                    Email = request.Email,
                    UserName = user.Username,
                },
                StatusCode = (int)HttpStatusCode.OK
            };
        }
    }
}
