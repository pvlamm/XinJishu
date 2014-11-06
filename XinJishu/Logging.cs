using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XinJishu
{
    public sealed class Logging
    {
        private static volatile Lazy<Logging> instance =
            new Lazy<Logging>(() => new Logging());
        private static Object syncRoot = new object();

        private Logging() { }

        public static Logging Instance { get{ return instance.Value; } }

        private LoggingType GetLoggingType()
        {
            String val = ConfigurationManager.AppSettings["Logging.StorageType"];
            return (LoggingType)Enum.Parse(typeof(LoggingType), val, true);
        }

        private String GetSqlConnection()
        {
            String val = ConfigurationManager.AppSettings["Logging.ConnectionStringName"];
            return ConfigurationManager.ConnectionStrings[val].ConnectionString;
        }

        public static void Log(LogMsgType type, String message)
        {

        }

        public static void Log(LogMsgType type, Exception e)
        {
            // xml creation
            var str = String.Format(@"<error><datetime>{0}</datetime><message>{1}</message><source>{2}</source><stacktrace>{3}</stacktrace></error>", DateTime.UtcNow, e.Message, e.Source, e.StackTrace);
            
        }

    }

    public enum LogMsgType
    {
        Information,
        Warning,
        Error
    }

    public enum LoggingType{
        Text,
        Email,
        Xml,
        Sql,
        SqlXml
    }
}
