using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace CosmicGameAPI.Utility.Hashing
{
    public class PasswordHasher
    {

        /// <summary>
        /// The initialize vector bytes
        /// </summary>
        private static readonly byte[] initVectorBytes;

        /// <summary>
        /// The keysize
        /// </summary>
        private static readonly int keysize;

        /// <summary>
        /// Initializes the <see cref="Cryption" /> class.
        /// </summary>
        static PasswordHasher()
        {
            //TODO: Get it from config file
            initVectorBytes = Encoding.ASCII.GetBytes("tu89geji340t89u2");
            keysize = 256;
        }

        /// <summary>
        /// Encrypts the specified plain text.
        /// </summary>
        /// <param name="plainText">The plain text.</param>
        /// <param name="privateKey">The pass phrase.</param>
        /// <returns></returns>
        public static string Encrypt(string plainText, string privateKey)
        {
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            using (PasswordDeriveBytes password = new PasswordDeriveBytes(privateKey, null))
            {
                byte[] keyBytes = password.GetBytes(keysize / 8);
                using RijndaelManaged symmetricKey = new();
                symmetricKey.Mode = CipherMode.CBC;
                using (ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes))
                {
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                        {
                            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                            cryptoStream.FlushFinalBlock();
                            byte[] cipherTextBytes = memoryStream.ToArray();
                            return Convert.ToBase64String(cipherTextBytes);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Decrypts the specified cipher text.
        /// </summary>
        /// <param name="cipherText">The cipher text.</param>
        /// <param name="privateKey">The pass phrase.</param>
        /// <returns></returns>
        public static string Decrypt(string cipherText, string privateKey)
        {
            byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
            using (PasswordDeriveBytes password = new PasswordDeriveBytes(privateKey, null))
            {
                byte[] keyBytes = password.GetBytes(keysize / 8);
                using RijndaelManaged symmetricKey = new();
                symmetricKey.Mode = CipherMode.CBC;
                using (ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes))
                {
                    using (MemoryStream memoryStream = new MemoryStream(cipherTextBytes))
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                        {
                            byte[] plainTextBytes = new byte[cipherTextBytes.Length];
                            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                        }
                    }
                }
            }
        }
    }
}
