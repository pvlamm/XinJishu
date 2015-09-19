using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XinJishu.Data;
using XinJishu.Web.Model;

namespace XinJishu.Web.Account
{
    public partial class UserManager : IDisposable
    {
        private String connectionStringName { get; set; }
        
        /// <summary>
        /// Assumes your DB ConnectionString name will be "default"
        /// </summary>    
        public UserManager() {
            connectionStringName = "default";
        }

        public UserManager(String connectionStringName)
        {
            this.connectionStringName = connectionStringName;
        }

        public Hashtable GetUserByEmail(String email)
        {
            using (XinJishu.Data.SQLServer.DataAccess conn = new Data.SQLServer.DataAccess(connectionStringName))
            {
                using (var cmd = conn.GetCommand())
                {
                    List<RoleModel> roles = new List<RoleModel>();

                    cmd.CommandText = "[xju].[User_GetByEmail]";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@email", email);

                    return conn.ExecuteHash(cmd).FirstOrDefault();
                }
            }
        }

        public void DeleteUserById(Int32 id)
        {
            using (XinJishu.Data.SQLServer.DataAccess conn = new Data.SQLServer.DataAccess(connectionStringName))
            {
                using (var cmd = conn.GetCommand())
                {
                    List<RoleModel> roles = new List<RoleModel>();

                    cmd.CommandText = "[xju].[User_DeleteUserById]";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }
            }

        }
        public void DeleteUserByPublicId(Guid publicId)
        {
            using (XinJishu.Data.SQLServer.DataAccess conn = new Data.SQLServer.DataAccess(connectionStringName))
            {
                using (var cmd = conn.GetCommand())
                {
                    List<RoleModel> roles = new List<RoleModel>();

                    cmd.CommandText = "[xju].[User_DeleteUserByPublicId]";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@publicId", publicId);

                    cmd.ExecuteNonQuery();
                }
            }

        }

        /// <summary>
        /// Updates a UserModel object against the database
        /// </summary>
        /// <param name="user"></param>
        public void UpdateUser(UserModel user)
        {
            using (XinJishu.Data.SQLServer.DataAccess conn = new Data.SQLServer.DataAccess(connectionStringName))
            {
                using (var cmd = conn.GetCommand())
                {
                    List<RoleModel> roles = new List<RoleModel>();

                    cmd.CommandText = "[xju].[User_Update]";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Clear();

                    cmd.Parameters.AddWithValue("@id", user.id);
                    cmd.Parameters.AddWithValue("@email", user.email);
                    cmd.Parameters.AddWithValue("@pwd", user.pwd);

                    foreach (var item in user.fields)
                        cmd.Parameters.AddWithValue("@" + item.Key, item.Value);

                    cmd.ExecuteNonQuery();
                }
            }

        }

        public void Dispose()
        {

        }
    }
}
