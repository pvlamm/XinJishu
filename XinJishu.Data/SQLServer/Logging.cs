using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XinJishu.Data.SQLServer
{
    public class Logging
    {
        private string connectionString { get; set; }
        private string name { get; set; }
        public Logging(string name, string connectionString) { }

        public void Log(LoggingType type, string message)
        {

        }
    }

    public enum LoggingType
    {
        Information,
        Important,
        Warning,
        Critical,
        Unknown
    }
}
