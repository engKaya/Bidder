using Bidder.UserService.Domain.Extensions;
using Bidder.UserService.Domain.Helper;

namespace Bidder.UserService.Domain.Models
{
    public class User : BaseEntity
    {
        private string _userName = string.Empty;
        private string _password = string.Empty;
        private string _email = string.Empty;
        private string _name = string.Empty;
        private string _surname = string.Empty;
        private string _address = string.Empty;
        private string _phoneNumber = string.Empty;

        public string  UserName
        {
            get => _userName;
            set => _userName = value;
        }
         public string Password
        {
            get => _password;
            protected set { 
                this._password = PasswordHasher.HashPassword(value);
            }
        }
  
        public string Email
        {
            get => _email;
            set => _email = value;
        }
        public string Name
        {
            get => _name;
            set => _name = value;
        }
        public string Surname
        {
            get => _surname;
            set => _surname = value;
        }
        public string Address
        {
            get => _address;
            set => _address = value;
        }
        public string PhoneNumber
        {
            get => _phoneNumber;
            set => _phoneNumber = value;
        }
        public User()
        {
        }
        public User(string userName, string password, string email, string name, string surname, string address, string phoneNumber)
        {
            _userName = userName;
            _password = PasswordHasher.HashPassword(password);
            _email = email;
            _name = name;
            _surname = surname;
            _address = address;
            _phoneNumber = phoneNumber;
            this.CreatedAt= DateTime.UtcNow; 
        }

        public void SetPassword(string password)
        {
            this._password = PasswordHasher.HashPassword(password);
        }

        public bool VerifyPassword(string password)
        {
            return PasswordHasher.VerifyHashedPassword(this._password, password);
        }
    }
}
