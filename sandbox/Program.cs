using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XinJishu.Security;

namespace sandbox
{
    public class Program
    {
        static void Main(string[] args)
        {
            Test();
            Test();
            Test();
        }

        public static void Test()
        {

            CryptoHelper ch = new CryptoHelper();

            String key = ch.GenerateKey();
            String iv = ch.GenerateIV();

            String sampleText = " When in Rome, do as the Romans ";

            Console.WriteLine("What have we generated? Let's See!");
            Console.WriteLine("----------------------------------");
            Console.WriteLine("Key: " + key);
            Console.WriteLine("IV : " + iv);
            Console.WriteLine("----------------------------------");

            Console.WriteLine("Text we are encrypting:");

            Console.WriteLine(sampleText);

            String crypted = ch.EncryptMessage(sampleText, key, iv);

            Console.WriteLine("Encrypted: " + crypted);

            String decrypted = ch.DecryptMessage(crypted, key, iv);

            Console.WriteLine("Decrypted: " + decrypted);

            Console.ReadLine();
        }
    }
}
