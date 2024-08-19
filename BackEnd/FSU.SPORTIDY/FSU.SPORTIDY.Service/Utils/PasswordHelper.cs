using System.Text;
using System.Security.Cryptography;
using BCrypt.Net;

namespace FSU.SPORTIDY.Service.Utils
{
    public class PasswordHelper
    {
       public static string HashPassword(string password)
       {
            return BCrypt.Net.BCrypt.EnhancedHashPassword(password, HashType.SHA256);
       }

       public static bool VerifyPassword(string password, string hashPassword)
       {
            return BCrypt.Net.BCrypt.EnhancedVerify(password, hashPassword, HashType.SHA256);
        }

       public static string GeneratePassword()
        {
            Random random = new Random();
            string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var password = new char[6];
            for (int i = 0; i < password.Length; i++)
            {
                int index = random.Next(allowedChars.Length);
                password[i] = allowedChars[index];
            }
            return new string(password);
        }

    }
}
