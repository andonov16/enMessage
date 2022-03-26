using System.Security.Cryptography;

namespace enMessage.Shared.Utilities
{
    public class KeyGenerator
    {
        private RSACryptoServiceProvider csp;



        public KeyGenerator()
        {
            csp = new RSACryptoServiceProvider(2048);
        }



        public RSAParameters GetPrivateKey()
        {
            return csp.ExportParameters(true);
        }

        public RSAParameters GetPublicKey()
        {
            return csp.ExportParameters(false);
        }
    }
}
