using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace XinJishu.Web.Google
{
    public class ReCaptchaHelper
    {
        private static string url = "https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}";

        public static CaptchaResponse Validate(string secret, string response)
        {
            var fullUrl = string.Format(url, secret, response);

            var client = new WebClient();
            var reply =
                client.DownloadString(
                    string.Format(url, secret, response));

            return JsonConvert.DeserializeObject<CaptchaResponse>(reply);

        }
    }

    public class CaptchaResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("error-codes")]
        public List<string> ErrorCodes { get; set; }
    }
}
