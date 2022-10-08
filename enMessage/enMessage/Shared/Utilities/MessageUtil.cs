using System.Security.Cryptography;

namespace enMessage.Shared.Utilities
{
    public static class MessageUtil
    {
        //Public key
        public static string EncryptMessage(RSAParameters publicKey, string data)
        {
            using (RSACryptoServiceProvider csp = new RSACryptoServiceProvider())
            {
                csp.ImportParameters(publicKey);
                var bytesData = System.Text.Encoding.Unicode.GetBytes(data);
                return Convert.ToBase64String(csp.Encrypt(bytesData, false));
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
