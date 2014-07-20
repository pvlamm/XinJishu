using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using XinJishu.Security;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using GeoCoding;
using System.Threading.Tasks;

namespace sandbox
{
    public class GeoAddress{
        public String Address { get; set; }
        public String City { get; set; }
        public String State { get; set; }
        public String Zip { get; set; }
        public float latitude { get; set; }
        public float longitude { get; set; }

        public override string ToString()
        {
            return (Address + "," + City + "," + State + " " + Zip);
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            //Test();

            //GeoAddress address = new GeoAddress();

            //address.Address = "6600 AAA Dr";
            //address.City = " Charlotte";
            //address.State = "NC";
            //address.Zip = "28212";

            //GeoCode(address);

            //String email = " , vorfeed@yahoo.com";
            //email = email.Trim();

            //if( email[0] == ',' )
            //{
            //    email = email.Substring(1, email.Length - 1).Trim();
            //}

            //Console.WriteLine(email);
            
            //RSATest();

            //Program program = new Program();


            //Console.WriteLine("This is a test...");

            //Task<String> results = program.RogerDogerAsync();

            //Console.WriteLine("Testing is over.");

            //Console.WriteLine(results.Result);

            GetAppointments_ByAdID("Test")
                .Select(record => record.ToString())
                .Distinct()
                .ToList()
                .ForEach(x => Console.WriteLine(x));


            Console.ReadLine();
        }

        public static List<String> GetAppointments_ByAdID(String str)
        {
            List<String> tmp = new List<string>();
            tmp.Add("ABC");
            tmp.Add("BCD");
            tmp.Add("CDE");
            tmp.Add("DEF");
            tmp.Add("EFG");
            tmp.Add("FGI");

            return tmp;
        }

        public async Task<String> RogerDogerAsync()
        {
            Console.WriteLine("Task - Roger Doger init test");

            Task<String> result = MamTestAsync();

            Console.WriteLine("Task - Compelte!");
            
            return await result;
        }

        public async Task<String> MamTestAsync()
        {
            Console.WriteLine("MemTest - Starting... ");
            await Task.Delay(5000);
            Console.WriteLine("MemTest - Completed... ");

            return "Hello World";
        }

        public static void RSATest()
        {
            RSAHelper helper = new RSAHelper();

            var key = helper.GenerateKey();

            String dataPackage = "This is a personal test of personal entertainment";

            String output = helper.Encrypt(dataPackage, key);

            Console.WriteLine(output);

            String decrypt_output = helper.Decrypt(output, key);

            Console.WriteLine(decrypt_output);


        }

        public static void GeoCode(GeoAddress address)
        {
            String googleApiKey = "AIzaSyBYAcW2e_C7SzAhOW408zhJltE8OYTtMJY";

            IGeoCoder geoCode = new GeoCoding.Google.GoogleGeoCoder();
            var addrs = geoCode.GeoCode(address.ToString());


            foreach (var adr in addrs)
            {
                Console.WriteLine("Latitude: " + adr.Coordinates.Latitude);
                Console.WriteLine("Longitude: " + adr.Coordinates.Longitude);
            }


            //GeoCoding.Google.GoogleGeoCoder ggc = new GeoCoding.Google.GoogleGeoCoder();

            //ggc.

            //WebClient wClient = new WebClient();

            //StreamReader sr = new StreamReader(wClient.OpenRead("https://maps.googleapis.com/maps/api/geocode/json?address=" + address.ToString().Replace(' ', '+') + "&sensor=false&key=" + googleApiKey));

            //String result = sr.ReadToEnd();

            //JObject obj = JObject.Parse(result);

            //JToken tok = obj["results"];
            //JToken tok2 = tok["geometry"];
            //JToken geo = tok2["location"];

            //Console.WriteLine("Latitude: " + (String)geo["lat"]);
            //Console.WriteLine("Long    : " + (String)geo["lng"]);

            //using (TextWriter tw = File.CreateText(@"C:\temp\testdata.txt"))
            //{
            //    tw.WriteLine(result);
            //}

            //Console.WriteLine(result);
            //?address=1600+Pennsylvania+Ave,+Washington+DC
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
