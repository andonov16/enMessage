using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EncryptingMessages
{
    public static class MessageUtil
    {
        //Public key
        public static byte[] EncryptMessage(RSAParameters publicKey, string data)
        {
            using(RSACryptoServiceProvider csp = new RSACryptoServiceProvider())
            {
                csp.ImportParameters(publicKey);
                var bytesData = System.Text.Encoding.Unicode.GetBytes(data);
                return csp.Encrypt(bytesData, false);
            }
        }

        //Private Key
        public static string DecryptMessage(RSAParameters privateKey, byte[] encryptedData)
        {
            using (RSACryptoServiceProvider csp = new RSACryptoServiceProvider())
            {
                csp.ImportParameters(privateKey);
                var bytesData = csp.Decrypt(encryptedData, false);
                return System.Text.Encoding.Unicode.GetString(bytesData);
            }
        }

        //Private Key
        public static byte[] DecryptMessageAsBytes(RSAParameters privateKey, byte[] encryptedData)
        {
            using (RSACryptoServiceProvider csp = new RSACryptoServiceProvider())
            {
                csp.ImportParameters(privateKey);
                return csp.Decrypt(encryptedData, false);
            }
        }
    }
}
