using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace CosmicGameAPI.Utility.Hashing
{
    public class stringHasher
    {
        private static byte[] key = { };
        private static byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xab, 0xcd, 0xef };

        public static string Decrypt(string stringToDecrypt, string sEncryptionKey)
        {
            stringToDecrypt = FromHex(stringToDecrypt);
            byte[] inputByteArray = new byte[stringToDecrypt.Length + 1];
            try
            {
                key = System.Text.Encoding.UTF8.GetBytes(sEncryptionKey);
                DESCryptoServiceProvider des = new();
                inputByteArray = Convert.FromBase64String(stringToDecrypt);
                MemoryStream ms = new();
                CryptoStream cs = new(ms, des.CreateDecryptor(key, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                System.Text.Encoding encoding = System.Text.Encoding.UTF8;
                return encoding.GetString(ms.ToArray());
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public static string Encrypt(string stringToEncrypt, string SEncryptionKey)
        {
            try
            {
                key = System.Text.Encoding.UTF8.GetBytes(SEncryptionKey);
                using DESCryptoServiceProvider des = new();
                byte[] inputByteArray = Encoding.UTF8.GetBytes(stringToEncrypt);
                MemoryStream ms = new();
                CryptoStream cs = new(ms, des.CreateEncryptor(key, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                return ToHex(Convert.ToBase64String(ms.ToArray()));
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }


        private static string ToHex(String input)
        {
            String hexOutput = "";
            char[] values = input.ToCharArray();
            foreach (char letter in values)
            {
                // Get the integral value of the character.
                int value = Convert.ToInt32(letter);
                // Convert the decimal value to a hexadecimal value in string form.
                hexOutput += String.Format("{0:X}", value);
            }
            return hexOutput;
        }

        public static String FromHex(string hex)
        {
            hex = hex.Replace("-", "");
            byte[] raw = new byte[hex.Length / 2];
            for (int i = 0; i < raw.Length; i++)
            {
                raw[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            }
            return Encoding.ASCII.GetString(raw); ;
        }
    }
}
