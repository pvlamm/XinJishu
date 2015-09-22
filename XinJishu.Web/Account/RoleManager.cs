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

namespace XinJishu.Web.Account
{
    public partial class RoleManager : IDisposable
    {
        
        private String connectionStringName { get; set; }
        
        /// <summary>
        /// Assumes your DB ConnectionString name will be "default"
        /// </summary>    
        public RoleManager() {
            connectionStringName = "default";
        }

        public RoleManager(String connectionStringName)
        {
            this.connectionStringName = connectionStringName;
        }

        public IList<Hashtable> GetRoles()
        {
            using (XinJishu.Data.SQLServer.DataAccess conn = new Data.SQLServer.DataAccess(connectionStringName))
            {
                using (var cmd = conn.GetCommand())
                {
                    List<RoleModel> roles = new List<RoleModel>();

                    cmd.CommandText = "[xju].[Roles_Get]";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    return conn.ExecuteHash(cmd);
                }
            }
        }
        public void InsertRole(Object name)
        {
            using (XinJishu.Data.SQLServer.DataAccess conn = new Data.SQLServer.DataAccess(connectionStringName))
            {
                using (var cmd = conn.GetCommand())
                {
                    List<RoleModel> roles = new List<RoleModel>();

                    cmd.CommandText = "[xju].[Roles_Insert]";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@name", name);

                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void DeleteRole(Object name)
        {
            using (XinJishu.Data.SQLServer.DataAccess conn = new Data.SQLServer.DataAccess(connectionStringName))
            {
                using (var cmd = conn.GetCommand())
                {
                    List<RoleModel> roles = new List<RoleModel>();

                    cmd.CommandText = "[xju].[Roles_Delete]";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@name", name);

                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void AddUserToRole(Object userId, Object roleId)
        {
            using (XinJishu.Data.SQLServer.DataAccess conn = new Data.SQLServer.DataAccess(connectionStringName))
            {
                using (var cmd = conn.GetCommand())
                {
                    List<RoleModel> roles = new List<RoleModel>();

                    cmd.CommandText = "[xju].[Roles_AddUserToRole]";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.Parameters.AddWithValue("@roleId", roleId);

                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void RemoveUserFromRole(Object userId, Object roleId)
        {
            using (XinJishu.Data.SQLServer.DataAccess conn = new Data.SQLServer.DataAccess(connectionStringName))
            {
                using (var cmd = conn.GetCommand())
                {
                    List<RoleModel> roles = new List<RoleModel>();

                    cmd.CommandText = "[xju].[Roles_RemoveUserFromRole]";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.Parameters.AddWithValue("@roleId", roleId);

                    cmd.ExecuteNonQuery();
                }
            }
        }
        public IList<Hashtable> ListRolesForUser(Object userId)
        {
            using (XinJishu.Data.SQLServer.DataAccess conn = new Data.SQLServer.DataAccess(connectionStringName))
            {
                using (var cmd = conn.GetCommand())
                {
                    List<RoleModel> roles = new List<RoleModel>();

                    cmd.CommandText = "[xju].[Roles_ListForUserId]";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@userId", userId);

                    return conn.ExecuteHash(cmd);
                }
            }
        } 

        public void Dispose()
        {
        }
    }
}
