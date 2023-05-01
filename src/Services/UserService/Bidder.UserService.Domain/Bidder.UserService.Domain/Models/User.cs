using Bidder.UserService.Domain.Extensions;

namespace Bidder.UserService.Domain.Models
{
    public class User : BaseEntity
    {

        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }

    }
}
