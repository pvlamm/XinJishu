using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
namespace XinJishu.Security
{
    public class CryptoHelper
    {
        private Rijndael crypto { get; set; }

        public CryptoHelper()
        {
            crypto = Rijndael.Create();
            crypto.Padding = PaddingMode.ISO10126;
            crypto.BlockSize = 256;
            crypto.KeySize = 256;
        }
        public String GenerateKey()
        {
            byte[] _key = new byte[crypto.KeySize];

            crypto.GenerateKey();

            _key = crypto.Key;

            String _keyString = Convert.ToBase64String(_key);

            return _keyString;
        }
        public String GenerateIV()
        {
            crypto.GenerateIV();

            byte[] _IV; // = new byte[crypto.IV.Length];
            
            _IV = crypto.IV;

            String _ivString = Convert.ToBase64String(_IV);

            return _ivString;
        }
        public String EncryptMessage(String message, String key, String IVstring)
        {
            //byte[] _key = Convert.FromBase64String(key);
            
            byte[] _key = Convert.FromBase64String(key); // ASCIIEncoding.UTF8.GetBytes(key);
            byte[] _IV = Convert.FromBase64String(IVstring); // ASCIIEncoding.UTF8.GetBytes(IVstring);

            if ( crypto.ValidKeySize(_key.Length ))
                throw new Exception("Error: Key Size does not match expectation");

            //if (_key.Length != crypto.KeySize)
            //    throw new Exception("Error: Key Size does not match expectation");

            String _encrypted = String.Empty;


            try
            {
                MemoryStream ms = new MemoryStream();

                using (CryptoStream cs = new CryptoStream(ms, crypto.CreateEncryptor(_key, _IV), CryptoStreamMode.Write))
                {
                    using (StreamWriter sw = new StreamWriter(cs))
                    {
                        sw.Write(message);
                        sw.Close();
                    }
                    cs.Close();
                }
                byte[] encoded = ms.ToArray();
                _encrypted = Convert.ToBase64String(encoded);

                ms.Close();
            }
            catch (CryptographicException e)
            {
                Console.WriteLine("A Cryptographic error occurred: {0}", e.Message);
                return null;
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine("A file error occurred: {0}", e.Message);
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: {0}", e.Message);
            }
            finally
            {
                crypto.Clear();
            }

            return _encrypted;
        }
        public String DecryptMessage(String cyphertext, String key, String iv)
        {
            string decrypted = String.Empty;

            try
            {
                //byte[] message = Convert.FromBase64String(cypher);
                byte[] message = Convert.FromBase64String(cyphertext);

                byte[] Key = Convert.FromBase64String(key);
                byte[] IV = Convert.FromBase64String(iv);

                crypto.Key = Key;
                crypto.IV = IV;

                MemoryStream ms = new MemoryStream(message);

                using (CryptoStream cs = new CryptoStream(ms, crypto.CreateDecryptor(Key, IV), CryptoStreamMode.Read))
                {
                    using (StreamReader sr = new StreamReader(cs))
                    {
                        decrypted = sr.ReadToEnd();
                    }
                }

            }
            finally
            {
                crypto.Clear();
            }

            return decrypted;
        }
    }
}
