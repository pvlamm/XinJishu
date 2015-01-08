using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XinJishu.Security;
using System.Text;

namespace SecurityTests
{
    [TestClass]
    public class DESSimpleEncryptionTest
    {
        private String _testTextFileContents = @"This book is intended for those who have read James Clavell's
Shogun and who are curious about its educational significance as 'A Novel of Japan.' Although Shogun, with its generous serving
of sex, violence, and intrigue, is in the mainstream of current popular entertainment, it is set apart by a certain instructional tone. For 
one thing, Shogun provides a wealth of factual information about Japanese history and culture, information which is probably new to
the majority of its readers. But Shogun is informative in a prescriptive sense as well, since the gradual acceptance of Japanese culture
by the hero Blackthorne bears the clear implication that the West has something to learn from Japan";

        private String file_name = "shogun.txt";
        private String encrypt_name = "encrypt_shogun";
        private String decrypt_name = "result_shogun.txt";

        private String _keyFileName = "key.txt";
        private String _IVFileName = "iv.txt";
        
        [TestMethod]
        public void TEST_ENCRYPT()
        {
            using (FileStream fs = new FileStream(file_name, FileMode.Create, FileAccess.ReadWrite))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.Write(_testTextFileContents);

                    Assert.IsTrue(File.Exists(file_name));
                }
            }

            ISimpleStreamEncryption crypto = new DESStreamEncryption();
            String key = crypto.GetKey();
            String iv = crypto.GetIV();

            using (FileStream fs = new FileStream(_keyFileName, FileMode.Create, FileAccess.ReadWrite))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.Write(key);
                    Assert.IsTrue(File.Exists(_keyFileName));
                }
            }

            using (FileStream fs = new FileStream(_IVFileName, FileMode.Create, FileAccess.ReadWrite))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.Write(iv);
                    Assert.IsTrue(File.Exists(_IVFileName));
                }
            }

            using (MemoryStream ms = new MemoryStream(Encoding.ASCII.GetBytes(_testTextFileContents)))
            {
                using (Stream encryptedStream = crypto.EncryptStream(ms, key, iv))
                {
                    encryptedStream.Seek(0, SeekOrigin.Begin);

                    MemoryStream eMS = new MemoryStream();
                    encryptedStream.CopyTo(eMS);



                    // On the fly decryption test!
                    String decryptedText = String.Empty;

                    using( Stream decryptedStream = crypto.DecryptStream(eMS, key, iv) ){
                        MemoryStream dMS = new MemoryStream();

                        decryptedStream.CopyTo(dMS);

                        decryptedText = Encoding.ASCII.GetString(dMS.ToArray());

                        Assert.AreEqual(_testTextFileContents, decryptedText);
                    }

                    encryptedStream.Seek(0, SeekOrigin.Begin);
                    using(FileStream fs = new FileStream(encrypt_name, FileMode.Create, FileAccess.Write ) ){
                        encryptedStream.CopyTo(fs);
                    }
                }
            }

        }

        [TestMethod]
        public void TEST_DECRYPT()
        {
            ISimpleStreamEncryption crypto = new DESStreamEncryption();
            String key = String.Empty;
            String iv = String.Empty;
            String decryptedText = String.Empty;

            using (FileStream fs = new FileStream(_keyFileName, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    key = sr.ReadToEnd();
                }
            }

            Assert.AreNotEqual(String.Empty, key);

            using (FileStream fs = new FileStream(_IVFileName, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    iv = sr.ReadToEnd();
                }
            }

            Assert.AreNotEqual(String.Empty, iv);

            using(FileStream fs = new FileStream(encrypt_name, FileMode.Open, FileAccess.Read) ){
                using (Stream decryptStream = crypto.DecryptStream(fs, key, iv))
                {
                    MemoryStream ms = new MemoryStream();
                    decryptStream.CopyTo(ms);

                    decryptedText = Encoding.ASCII.GetString(ms.ToArray());

                    Assert.AreEqual(_testTextFileContents, decryptedText);
                }
            }

        }


    }
}
