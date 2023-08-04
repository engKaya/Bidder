using Bidder.IdentityService.Application.Interfaces.Repos;
using Bidder.IdentityService.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Bidder.IdentityService.Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TestController : ControllerBase
    { 
        private readonly IServiceScopeFactory factory;

        public TestController(IServiceScopeFactory factory)
        {
            this.factory = factory;
        }

        [HttpGet]
        [ActionName("Test")]
        public async void Test()
        {
            Users user = new Users();
            user.Name = "test";
            user.Surname = "test";
            user.Username = "test"; 
            user.Email = "test@gmailc.com";
            user.PhoneNumber = "+93212063546";
            user.IdentityNumber = "111111111";

            user.SetPassword("test");
            using (var scope = factory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<IUserRepository>();
                await context.AddAsync(user);
                await context.UnitOfWork.SaveEntitiesAsync();
            }
            //await repo.AddAsync(user);
            //await repo.UnitOfWork.SaveEntitiesAsync();
        }
    }
}