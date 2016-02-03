using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using XinJishu.Data.SQLServer;

namespace XinJishu.Net.Email
{
    public class MessageModel
    {
        public MessageModel() { }
        public MessageModel(Int32 id) { this.id = id; }

        public Int32 id { get; private set; }
        public Int32 public_id { get; set; }
        public String to { get; set; }
        public String cc { get; set; }
        public String bcc { get; set; }
        public String from { get; set; }
        public String subject { get; set; }
        public String body { get; set; }
        public DateTime create_date { get; set; }
        public DateTime sent_date { get; set; }
        public String status { get; set; }
    }


    public class EmailManager
    {
        public EmailManager()
        {

        }

        private String email_host { get { return ConfigurationManager.AppSettings["email.host"]; } }
        private Int32 email_port { get { return Convert.ToInt32( ConfigurationManager.AppSettings["email.port"] ); } }
        private Boolean email_use_ssl { get { return Convert.ToBoolean(ConfigurationManager.AppSettings["email.usessl"]); } }
        private String email_username { get { return ConfigurationManager.AppSettings["email.username"]; } }
        private String email_password { get { return ConfigurationManager.AppSettings["email.password"]; } }

        public Boolean SendMessage(String to, String cc, String bcc, String from,
            String subject, String body)
        {
            var message = new MessageModel()
            {
                to = to,
                cc = cc,
                bcc = bcc,
                from = from,
                subject = subject,
                body = body,
                status = String.Empty
            };

            return SendMessage(message);
        }

        public Boolean SendMessage(MessageModel msg)
        {
            bool result = false;

            String email = msg.to;
            email = email.Trim();

            var smtp = new SmtpClient
            {
                Host = email_host,
                Port = email_port,
                EnableSsl = email_use_ssl,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(email_username, email_password)
            };

            try
            {
                using (var message = new MailMessage(msg.from, msg.to)
                {
                    IsBodyHtml = true,
                    Subject = msg.subject,
                    Body = msg.body
                })
                {
                    if( msg.cc.Length > 0)
                        foreach (String cc in msg.cc.Split(','))
                            message.CC.Add(new MailAddress(cc));

                    if( msg.bcc.Length > 0)
                        foreach (String bcc in msg.bcc.Split(','))
                            message.Bcc.Add(new MailAddress(bcc));

                    smtp.Send(message);
                    result = true;
                }

            }
            catch (SmtpException smtp_error)
            {
                throw smtp_error;
            }
            catch (ArgumentNullException argument_null_error)
            {
                throw argument_null_error;
            }


            return result;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="MessageId">Must be passed from the Caller.</param>
        /// <param name="To"></param>
        /// <param name="From"></param>
        /// <param name="Subject"></param>
        /// <param name="Body"></param>
        /// <param name="MessageType"></param>
        /// <returns></returns>
        private Guid InsertMessage(Guid MessageId, String To, String From, String Subject, String Body, String MessageType)
        {
            using (DataAccess access = DataAccess.Create(this.ConnectionStringName))
            {

                try
                {
                    if (IsEmailToFieldValid(To))
                    {
                        access.CreateProcedureCommand("[dbo].[Message_Create]");
                        access.AddParameter("@ToAddress", To, System.Data.ParameterDirection.Input);
                        access.AddParameter("@FromAddress", From, System.Data.ParameterDirection.Input);
                        access.AddParameter("@Subject", Subject, System.Data.ParameterDirection.Input);
                        access.AddParameter("@Body", Body, System.Data.ParameterDirection.Input);
                        access.AddParameter("@MessageType", MessageType, System.Data.ParameterDirection.Input);
                        access.AddParameter("@QueDate", DateTime.Now, System.Data.ParameterDirection.Input);
                        access.AddParameter("@SentDate", null, System.Data.ParameterDirection.Input);
                        access.AddParameter("@MessageStatus", "QUED", System.Data.ParameterDirection.Input);
                        access.AddParameter("@PublicID", MessageId, System.Data.ParameterDirection.Input);
                        access.ExecuteHash();
                    }
                }
                catch (System.Data.DataException data_error)
                {
                    this.Logging.LogException(data_error);
                }
                catch (System.Data.SqlClient.SqlException sql_error)
                {
                    this.Logging.LogException(sql_error);
                }
                finally
                {
                    if (access != null)
                    {
                        access.Dispose();
                    }
                }
            }
            return MessageId;
        }

        private bool UpdateMessage(Guid MessageId, bool sent)
        {
            bool result = false;
            using (DataAccess access = DataAccess.Create(this.ConnectionStringName))
            {

                try
                {
                    access.CreateProcedureCommand("[dbo].[Message_UpdateSent]");
                    access.AddParameter("@PublicId", MessageId, System.Data.ParameterDirection.Input);
                    access.AddParameter("@Sent", sent, System.Data.ParameterDirection.Input);
                    access.ExecuteHash();

                    result = true;
                }
                catch (System.Data.DataException data_error)
                {
                    this.Logging.LogDetailedException(new BaseException(this.GetObjectContext(), data_error, "Error updating sent message."));
                    throw data_error;
                }
                catch (System.Data.SqlClient.SqlException sql_error)
                {
                    this.Logging.LogDetailedException(new BaseException(this.GetObjectContext(), sql_error, "Error updating sent message."));
                    throw sql_error;
                }
                finally
                {
                    if (access != null)
                    {
                        access.Dispose();
                    }
                }
            }
            return result;
        }

        private bool DeleteMessage(Guid MessageId)
        {
            bool result = false;
            using (DataAccess access = DataAccess.Create(this.ConnectionStringName))
            {

                try
                {
                    access.CreateProcedureCommand("[dbo].[Message_Delete]");
                    access.AddParameter("@PublicId", MessageId, System.Data.ParameterDirection.Input);
                    access.ExecuteHash();
                    result = true;
                }
                catch (System.Data.SqlClient.SqlException sql_error)
                {
                    result = false;
                    throw sql_error;
                }
                catch (System.Data.DataException data_error)
                {
                    result = false;
                    throw data_error;
                }
                finally
                {
                    if (access != null)
                    {
                        access.Dispose();
                    }
                }
            }
            return result;
        }

    }
}
