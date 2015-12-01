using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace XinJishu.Web.Google
{
    public class SimpleMail
    {
        private string from { get { return ConfigurationManager.AppSettings["emailFrom"]; } }
        private string fromName { get { return ConfigurationManager.AppSettings["emailFromName"]; } }
        private string password { get { return ConfigurationManager.AppSettings["emailPassword"]; } }
        public bool Send(string subject, string body, string to, string toName = "" ){

            if (string.IsNullOrWhiteSpace(toName))
                toName = to;

            var fromAddress = new MailAddress(from, fromName);
            var toAddress = new MailAddress(to, toName);

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, password)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            }){
                try
                {
                    smtp.Send(message);
                    return true;
                }
                catch
                {
                    return false;
                }
            }

        }
    }
}
