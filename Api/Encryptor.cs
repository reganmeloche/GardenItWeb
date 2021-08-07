using System;
using System.Security.Cryptography;
using System.Text;

namespace gardenit_web.Api
{
    public interface IEncryptor {
        (string encryptedValue, string iv)  Encrypt(string encryptionKey, string valueToEncrypt);
    }
    public class Encryptor : IEncryptor
    {
        public (string encryptedValue, string iv) Encrypt(string encryptionKey, string valueToEncrypt) {
            var bytArr = Encoding.UTF8.GetBytes(encryptionKey);
            var key = Convert.ToBase64String(bytArr);
            Aes cipher = CreateCipher(key);
            var iv = Convert.ToBase64String(cipher.IV);

            ICryptoTransform cryptTransform = cipher.CreateEncryptor();
            byte[] plaintext = Encoding.UTF8.GetBytes(valueToEncrypt);
            byte[] cipherText = cryptTransform.TransformFinalBlock(plaintext, 0, plaintext.Length);
        
            return (Convert.ToBase64String(cipherText), iv);
        }

        
        private static Aes CreateCipher(string keyBase64)
        {
            // Default values: Keysize 256, Padding PKC27
            Aes cipher = Aes.Create();
            cipher.Mode = CipherMode.CBC;  // Ensure the integrity of the ciphertext if using CBC
        
            cipher.Padding = PaddingMode.ISO10126;
            cipher.Key = Convert.FromBase64String(keyBase64);
        
            return cipher;
        }
    }
}