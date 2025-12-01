using System.Security.Cryptography;
using System.Text;

namespace Snowly.Application.Helpers
{
    public static class PasswordHasher
    {
        /// <summary>
        /// Şifre HASHleme işlemi yapar
        /// </summary>
        /// <param name="password">Kullanıcının kayıt esnasındaki şifresi</param>
        /// <returns></returns>
        public static string Hash(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        /// <summary>
        /// Şifre Doğru mu kontrolü
        /// </summary>
        /// <param name="hashedPassword">Kullanıcının HASHlenmiş şifresi</param>
        /// <param name="providedPassword">Input'a girilen şifre</param>
        /// <returns></returns>
        public static bool Verify(string hashedPassword, string providedPassword)
        {
            var hashOfInput = Hash(providedPassword);
            return hashOfInput == hashedPassword;
        }
    }
}
