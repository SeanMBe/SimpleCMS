using System;
using System.Security.Cryptography;
using System.Text;
using SimpleCMS.Core.Data;
using SimpleCMS.Core.Models;

namespace SimpleCMS.Core.Services
{
    public class AccountService
    {
        readonly IRepository repository;

        public AccountService(IRepository repository)
        {
            this.repository = repository;
        }

        public bool Authenticate(string username, string password)
        {
            var user = repository.Find<Account>(x => x.Email == username);
            var saltedPassword = GenerateSaltedPassword(password, user.PasswordSalt);
            return user.EncryptedPassword == saltedPassword;
        }

        public Account Create(string email, string password)
        {
            var salt = GenerateSalt(40);
            var user = new Account
            {
                Email = email,
                EncryptedPassword = GenerateSaltedPassword(password, salt),
                PasswordSalt = salt
            };

            repository.Save(user);

            return user;
        }

        public string GenerateSalt(int bufferSize)
        {
            var randomNumber = new RNGCryptoServiceProvider();
            var buffer = new byte[bufferSize];
            randomNumber.GetBytes(buffer);

            return Convert.ToBase64String(buffer);
        }

        public string GenerateSaltedPassword(string password, string salt)
        {
            var plainTextWithSalt = String.Concat(password, salt);
            var plainTextWithSaltBytes = Encoding.UTF8.GetBytes(plainTextWithSalt);

            HashAlgorithm algorithm = new SHA256Managed();
            var hash = algorithm.ComputeHash(plainTextWithSaltBytes);

            return Convert.ToBase64String(hash);
        }
    }
}