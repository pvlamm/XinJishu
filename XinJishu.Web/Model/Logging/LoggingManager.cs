using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace XinJishu.Web.Logging
{
    public partial class LoggingManager : IDisposable
    {
        private String connectionStringName { get; set; }
        
        /// <summary>
        /// Assumes your DB ConnectionString name will be "default"
        /// </summary>    
        public LoggingManager() {
            connectionStringName = "default";
        }

        public LoggingManager(String connectionStringName)
        {
            this.connectionStringName = connectionStringName;
        }

        public void Log()
        {
            if (HttpContext.Current != null)
            {
                var ipAddress = HttpContext.Current.Request.UserHostAddress;
                var dnsName = HttpContext.Current.Request.UserHostName;
                var langs = HttpContext.Current.Request.UserLanguages;
                var requestUrl = HttpContext.Current.Request.RawUrl;
                var requestParams = HttpContext.Current.Request.Params;
                var requestType = HttpContext.Current.Request.RequestType;

                using (XinJishu.Data.SQLServer.DataAccess conn = new Data.SQLServer.DataAccess(connectionStringName))
                {
                    using (var cmd = conn.GetCommand())
                    {
                        cmd.CommandText = "[xju].[WebRequest_Insert]";
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.ExecuteNonQuery();
                    }
                }

            }
        }

        public void Dispose()
        {
        }
    }
}
