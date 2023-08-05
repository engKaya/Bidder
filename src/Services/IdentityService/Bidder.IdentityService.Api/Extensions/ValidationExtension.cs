using Bidder.IdentityService.Domain.DTOs.User.Request;
using Bidder.IdentityService.Infastructure.Validations;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace Bidder.IdentityService.Api.Extensions
{
    public static class ValidationExtension
    {
        public static IServiceCollection ConfigureValidation(this IServiceCollection services)
        {
            services.AddScoped<IValidator<CreateUserRequest>, CreateUserRequestValidation>(); 
            services.AddFluentValidation(s =>
            {
                s.RegisterValidatorsFromAssemblyContaining<CreateUserRequestValidation>();
            });
            services.Configure<ApiBehaviorOptions>(opt =>
            {
                opt.SuppressModelStateInvalidFilter= true;
            });

            return services;    
        }
    }
}
