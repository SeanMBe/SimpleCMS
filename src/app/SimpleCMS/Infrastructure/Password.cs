using System;
using System.Security.Cryptography;
using System.Text;

namespace SimpleCMS.Infrastructure
{
    public static class Password
    {
        public static string GenerateSalt(int bufferSize)
        {
            var randomNumber = new RNGCryptoServiceProvider();
            var buffer = new byte[bufferSize];
            randomNumber.GetBytes(buffer);

            return Convert.ToBase64String(buffer);
        }

        public static string GenerateSaltedPassword(string password, string salt)
        {
            var plainTextWithSalt = String.Concat(password, salt);
            var plainTextWithSaltBytes = Encoding.UTF8.GetBytes(plainTextWithSalt);

            HashAlgorithm algorithm = new SHA256Managed();
            var hash = algorithm.ComputeHash(plainTextWithSaltBytes);

            return Convert.ToBase64String(hash);
        }
    }
}