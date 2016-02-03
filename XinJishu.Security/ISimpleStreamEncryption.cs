using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace XinJishu.Security
{
    public interface ISimpleStreamEncryption
    {
        String GetKey();
        String GetIV();
        Stream EncryptStream(Stream sToEncrypt, String sKey, String sIV);
        Stream DecryptStream(Stream sToDecrypt, String sKey, String sIV);

    }
}