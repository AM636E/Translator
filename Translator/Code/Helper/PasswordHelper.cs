using System;
using System.Security.Cryptography;
using System.Text;

namespace Translator.Code.Helper
{
    public static class PasswordHelper
    {
        public static string GetHash(string password)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(password);
            byte[] hash = SHA256.Create().ComputeHash(bytes);

            string hashedPassword = Encoding.ASCII.GetString(hash);

            return hashedPassword;
        }
    }
}