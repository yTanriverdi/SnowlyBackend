using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Snowly.Application.MessageSecure
{
    public  class MessageCrypto
    {
        private readonly string? _key;

        public MessageCrypto(IConfiguration configuration)
        {
            _key = configuration["CryptoSettings:EncryptionKey"];
        }

        /// <summary>
        /// Mesajı şifrelemek için kullan
        /// </summary>
        /// <param name="message">Mesaj içeriği</param>
        /// <returns></returns>
        public string Encrypt(string message)
        {
            using var aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(_key!);
            aes.GenerateIV();

            using var encryptor = aes.CreateEncryptor();
            byte[] plainBytes = Encoding.UTF8.GetBytes(message);
            byte[] cipherBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

            byte[] result = aes.IV.Concat(cipherBytes).ToArray();
            return Convert.ToBase64String(result);
        }


        /// <summary>
        /// Mesajın şifresini açmak için kullan
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public string Decrypt(string message)
        {
            byte[] fullCipher = Convert.FromBase64String(message);
            using var aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(_key!);

            byte[] iv = fullCipher[..16];
            byte[] cipher = fullCipher[16..];

            aes.IV = iv;
            using var decryptor = aes.CreateDecryptor();
            byte[] decryptedBytes = decryptor.TransformFinalBlock(cipher, 0, cipher.Length);
            return Encoding.UTF8.GetString(decryptedBytes);
        }
    }
}
