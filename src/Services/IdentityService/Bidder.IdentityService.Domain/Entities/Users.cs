using Bidder.IdentityService.Domain.Helper;
using System.ComponentModel.DataAnnotations;

namespace Bidder.IdentityService.Domain.Entities
{
    public class Users : BaseEntity
    {
        private string _name = string.Empty;
        private string _surname = string.Empty;
        private string _email = string.Empty;
        private string _username = string.Empty;
        private string _password = string.Empty;
        private string _phoneNumber = string.Empty;
        private string _identityNumber = string.Empty;
        private bool _isEmailVerified = false;
        private DateTime? _emailVerifiedAt; 
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
        [EmailAddress]
        public string Email
        {
            get => _email;
            set => _email = value;
        }
        public string Username
        {
            get => _username;
            set => _username = value;
        }
        public string Password
        {
            get => _password;
            protected set  => this._password = PasswordHasher.HashPassword(value); 
        }
        public string PhoneNumber
        {
            get => _phoneNumber;
            set => _phoneNumber = value;
        }
        public string IdentityNumber
        {
            get => _identityNumber;
            set => _identityNumber = value;
        }
        public bool  IsEmailVerified
        {
            get => _isEmailVerified;
            set => _isEmailVerified = value;
        }
        public DateTime? EmailVerifiedAt
        {
            get => _emailVerifiedAt;
            set => _emailVerifiedAt = value;
        } 
        public Users()
        {

        }

        public Users(string email, string password, string name, string surname)
        {
            Email = email;
            Password = password;
            IsEmailVerified = false;
            Name = name;
            Surname = surname;
            Username = "Anon" + DateTime.Now.Ticks.ToString();
            CreatedAt = DateTime.Now;
        }

        public void SetPassword(string password)
        {
            this._password = PasswordHasher.HashPassword(password);
        }

        /// <summary>
        /// Eğer verilen Şifre ile Hashlenmiş şifre doğru ise  "True" döner.
        /// </summary>
        /// <param name="password">Dışarıdan gelen açık şifre</param>
        /// <returns>Boolean</returns>
        public bool VerifyPassword(string password)
        {
            return PasswordHasher.VerifyHashedPassword(this._password, password);
        }
    }
}
