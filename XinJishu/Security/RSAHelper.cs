using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace XinJishu.Security
{
    public class RSAHelper
    {
        private RSACryptoServiceProvider crypto { get; set; }
        private RSAParameters RSAKeyInfo { get; set; }
        private Int32 _keySize { get { return 2048; } }
        private UnicodeEncoding ByteConverter = new UnicodeEncoding();
        public RSAHelper()
        {
        }

        public RSAParameters GenerateKey()
        {
            using (crypto = new RSACryptoServiceProvider(_keySize))
            {
                crypto.KeySize = _keySize;

                crypto.PersistKeyInCsp = false;

                return crypto.ExportParameters(false);
            }

        }

        public String Encrypt(String data, RSAParameters key)
        {
            byte[] bData = ByteConverter.GetBytes(data);
            
            return Convert.ToBase64String(RSAEncrypt(bData, key, true));
        }

        public String Decrypt(String data, RSAParameters key)
        {
            byte[] bData = Encoding.ASCII.GetBytes(data);
            
            return ByteConverter.GetString(RSADecrypt(bData, key, true));

        }
        internal byte[] RSAEncrypt(byte[] DataToEncrypt, RSAParameters RSAKeyInfo, bool DoOAEPPadding)
        {
            try
            {
                byte[] encryptedData;
                //Create a new instance of RSACryptoServiceProvider. 
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(_keySize))
                {
                    //Import the RSA Key information. This only needs 
                    //toinclude the public key information.
                    //RSA.ImportParameters(RSAKeyInfo);
                    
                    //Encrypt the passed byte array and specify OAEP padding.   
                    //OAEP padding is only available on Microsoft Windows XP or 
                    //later.  
                    encryptedData = RSA.Encrypt(DataToEncrypt, DoOAEPPadding);
                }
                return encryptedData;
            }
            //Catch and display a CryptographicException   
            //to the console. 
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);

                return null;
            }

        }

        internal byte[] RSADecrypt(byte[] DataToDecrypt, RSAParameters RSAKeyInfo, bool DoOAEPPadding)
        {
            try
            {
                byte[] decryptedData;
                //Create a new instance of RSACryptoServiceProvider. 
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(_keySize))
                {
                    //Import the RSA Key information. This needs 
                    //to include the private key information.
                    RSA.ImportParameters(RSAKeyInfo);

                    //Decrypt the passed byte array and specify OAEP padding.   
                    //OAEP padding is only available on Microsoft Windows XP or 
                    //later.  
                    decryptedData = RSA.Decrypt(DataToDecrypt, DoOAEPPadding);
                }
                return decryptedData;
            }
            //Catch and display a CryptographicException   
            //to the console. 
            catch (CryptographicException e)
            {
                Console.WriteLine(e.ToString());

                return null;
            }

        }
    }
}
