using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XinJishu.Data.SQLServer;
using XinJishu.Web.Model;


namespace XinJishu.Web.Session
{
    public class SessionManager : IDisposable
    {
        private String connectionStringName { get; set; }
        
        /// <summary>
        /// Assumes your DB ConnectionString name will be "default"
        /// </summary>    
        public SessionManager() {
            connectionStringName = "default";
        }

        public SessionManager(String connectionStringName)
        {
            this.connectionStringName = connectionStringName;
        }

        public IList<Hashtable> ListSessions()
        {
            using (XinJishu.Data.SQLServer.DataAccess conn = new Data.SQLServer.DataAccess(connectionStringName))
            {
                using (var cmd = conn.GetCommand())
                {
                    List<RoleModel> roles = new List<RoleModel>();

                    cmd.CommandText = "[xju].[Session_List]";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    return conn.ExecuteHash(cmd);
                }
            }
        }
        public IList<Hashtable> ListSessionsForUsername(Object username)
        {
            using (XinJishu.Data.SQLServer.DataAccess conn = new Data.SQLServer.DataAccess(connectionStringName))
            {
                using (var cmd = conn.GetCommand())
                {
                    List<RoleModel> roles = new List<RoleModel>();

                    cmd.CommandText = "[xju].[Session_ListByUsername]";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@username", username);

                    return conn.ExecuteHash(cmd);
                }
            }
        }

        public Hashtable CreateSession(Object username)
        {

            using (XinJishu.Data.SQLServer.DataAccess conn = new Data.SQLServer.DataAccess(connectionStringName))
            {
                using (var cmd = conn.GetCommand())
                {
                    List<RoleModel> roles = new List<RoleModel>();

                    cmd.CommandText = "[xju].[Session_Create]";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@username", username);

                    return conn.ExecuteHash(cmd).FirstOrDefault();
                }
            }
        }
        public Hashtable UpdateSession(Object username)
        {

            using (XinJishu.Data.SQLServer.DataAccess conn = new Data.SQLServer.DataAccess(connectionStringName))
            {
                using (var cmd = conn.GetCommand())
                {
                    List<RoleModel> roles = new List<RoleModel>();

                    cmd.CommandText = "[xju].[Session_Update]";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@username", username);

                    return conn.ExecuteHash(cmd).FirstOrDefault();
                }
            }
        }

        public Hashtable DeleteSessionById(Int32 id)
        {

            using (XinJishu.Data.SQLServer.DataAccess conn = new Data.SQLServer.DataAccess(connectionStringName))
            {
                using (var cmd = conn.GetCommand())
                {
                    List<RoleModel> roles = new List<RoleModel>();

                    cmd.CommandText = "[xju].[Session_Delete]";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@id", id);

                    return conn.ExecuteHash(cmd).FirstOrDefault();                
                }
            }
        }

        public void DeleteAllSession()
        {

            using (XinJishu.Data.SQLServer.DataAccess conn = new Data.SQLServer.DataAccess(connectionStringName))
            {
                using (var cmd = conn.GetCommand())
                {
                    List<RoleModel> roles = new List<RoleModel>();

                    cmd.CommandText = "[xju].[Session_DeleteAll]";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Dispose()
        {
        }
    }
}
