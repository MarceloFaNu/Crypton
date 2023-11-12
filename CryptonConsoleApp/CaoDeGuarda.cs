using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CryptonConsoleApp
{
    internal class CaoDeGuarda
    {
        // Caracteres gerados pelo site Random.Org
        private static string qwerty = "hpol6QwMxy2SYNeD";
        private static string qwertyuiop = "pPciqcrf";

        public static string Qwerty
        {
            get { return qwerty; }
            set { qwerty = value; }
        }

        public static string EncryptString(string plainText)
        {
            byte[] encrypted;
            byte[] key = Encoding.UTF8.GetBytes(qwerty);
            byte[] iv = Encoding.UTF8.GetBytes(qwertyuiop);

            // Create a TripleDES object with the specified key and IV.
            using (TripleDES tripleDES = TripleDES.Create())
            {
                tripleDES.Key = key;
                tripleDES.IV = iv;

                // Create a new MemoryStream object to contain the encrypted bytes.
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    // Create a CryptoStream object to perform the encryption.
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, tripleDES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        // Encrypt the plaintext.
                        using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                        {
                            streamWriter.Write(plainText);
                        }

                        encrypted = memoryStream.ToArray();
                    }
                }
            }

            var resultado = Convert.ToBase64String(encrypted);
            return resultado;
        }

        public static string DecryptString(string encryptedPlainText)
        {
            string decrypted;
            byte[] cipherText = Convert.FromBase64String(encryptedPlainText);
            byte[] key = Encoding.UTF8.GetBytes(qwerty);
            byte[] iv = Encoding.UTF8.GetBytes(qwertyuiop);

            // Create a TripleDES object with the specified key and IV.
            using (TripleDES tripleDES = TripleDES.Create())
            {
                tripleDES.Key = key;
                tripleDES.IV = iv;

                // Create a new MemoryStream object to contain the decrypted bytes.
                using (MemoryStream memoryStream = new MemoryStream(cipherText))
                {
                    // Create a CryptoStream object to perform the decryption.
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, tripleDES.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        // Decrypt the ciphertext.
                        using (StreamReader streamReader = new StreamReader(cryptoStream))
                        {
                        decrypted = streamReader.ReadToEnd();
                        }
                    }
                }
            }

            return decrypted;
        }
    }
}
