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
                    cmd.Parameters.AddWithValue("@name", email);

                    return conn.ExecuteHash(cmd).FirstOrDefault();
                }
            }
        }

        public void DeleteUserById(Int32 id)
        {
            using (XinJishu.Data.SQLServer.DataAccess conn = new Data.SQLServer.DataAccess(connectionStringName))
            {

            }

        }
        public void DeleteUserByPublicId(Guid id)
        {
            using (XinJishu.Data.SQLServer.DataAccess conn = new Data.SQLServer.DataAccess(connectionStringName))
            {

            }

        }
        public void UpdateUser(UserModel account)
        {
            using (XinJishu.Data.SQLServer.DataAccess conn = new Data.SQLServer.DataAccess(connectionStringName))
            {

            }

        }

        public void Dispose()
        {

        }
    }
}
