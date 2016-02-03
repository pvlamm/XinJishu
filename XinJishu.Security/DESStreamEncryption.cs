using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace XinJishu.Security
{
    /// <summary>
    /// Implements ISimpleStreamEncryption
    /// </summary>
    public class DESStreamEncryption : ISimpleStreamEncryption
    {
        private DESCryptoServiceProvider _des = new DESCryptoServiceProvider();

        public DESStreamEncryption()
        {
            _des.GenerateIV();
            _des.GenerateKey();
            _des.Padding = PaddingMode.None;

        }

        /// <summary>
        /// Implements a new Key on each new DESStreamEncryption called
        /// </summary>
        /// <returns>Base64String of Key</returns>
        public string GetKey()
        {
            return Convert.ToBase64String(_des.Key);
        }

        /// <summary>
        /// Implements a new Key on each new DESStreamEncryption called
        /// </summary>
        /// <returns>Base64String of Key</returns>
        public string GetIV()
        {
            return Convert.ToBase64String(_des.IV);
        }

        /// <summary>
        /// Provided a Stream to Encrypt, and Base64 encoded Key and IV,
        /// returns an Encrypted Stream.
        /// </summary>
        /// <param name="sToEncrypt"></param>
        /// <param name="sKey">Base64 encoded String</param>
        /// <param name="sIV">Base64 encoded String</param>
        /// <returns>EncryptedStream</returns>
        public System.IO.Stream EncryptStream(System.IO.Stream sToEncrypt, string sKey, string sIV)
        {
            sToEncrypt.Seek(0, SeekOrigin.Begin);

            byte[] _key = Convert.FromBase64String(sKey);
            byte[] _IV = Convert.FromBase64String(sIV);

            ICryptoTransform desencrypt = _des.CreateEncryptor(_key, _IV);
            MemoryStream msEncrypted = new MemoryStream();

            CryptoStream cryptostream = new CryptoStream(msEncrypted, desencrypt, CryptoStreamMode.Write);

            int data;
            while ((data = sToEncrypt.ReadByte()) != -1)
                cryptostream.WriteByte((byte)data);

            //byte[] bytearrayinput = new byte[sToEncrypt.Length];
            //sToEncrypt.Read(bytearrayinput, 0, bytearrayinput.Length);
            //cryptostream.Write(bytearrayinput, 0, bytearrayinput.Length);
            msEncrypted.Seek(0, SeekOrigin.Begin);

            return msEncrypted;
        }

        public System.IO.Stream DecryptStream(System.IO.Stream sToDecrypt, string sKey, string sIV)
        {
            // You cannot believe how badly this _CAN_ burn you if you don't have it... >.>
            sToDecrypt.Seek(0, SeekOrigin.Begin);

            byte[] _key = Convert.FromBase64String(sKey);
            byte[] _IV = Convert.FromBase64String(sIV);

            ICryptoTransform desencrypt = _des.CreateDecryptor(_key, _IV);
            
            CryptoStream cryptostream = new CryptoStream(sToDecrypt, desencrypt, CryptoStreamMode.Read);

            MemoryStream msDecrypted = new MemoryStream();

            int data;
            while ((data = cryptostream.ReadByte()) != -1)
                msDecrypted.WriteByte((byte)data);

            msDecrypted.Seek(0, SeekOrigin.Begin);

            return msDecrypted;
        }
    }
}
