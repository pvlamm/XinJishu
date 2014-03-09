using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptSharp;

namespace XinJishu.Security
{
    public class PasswordManager
    {
        public static String BCryptEncrypt(String password)
        {
            return Crypter.Blowfish.Crypt(password, Crypter.Blowfish.GenerateSalt());
        }

        public static Boolean BCryptVerifyPassword(String password, String hashed_password)
        {
            return Crypter.CheckPassword(password, hashed_password);
        }
    }
}
