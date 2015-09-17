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
    public partial class AccountManager : IDisposable
    {
        private String connectionStringName { get; set; }
        
        /// <summary>
        /// Assumes your DB ConnectionString name will be "default"
        /// </summary>    
        public AccountManager() {
            connectionStringName = "default";
        }

        public AccountManager(String connectionStringName)
        {
            this.connectionStringName = connectionStringName;
        }

        public Boolean Authenticate(String email, String password)
        {
            // Test email if valid email address or not

            if (String.IsNullOrWhiteSpace(password))
                return false;

            using (XinJishu.Data.SQLServer.DataAccess conn = new Data.SQLServer.DataAccess(connectionStringName))
            {

            }

            return true;
        }

        public Hashtable GetAccountByEmail(String email)
        {
            using (XinJishu.Data.SQLServer.DataAccess conn = new Data.SQLServer.DataAccess(connectionStringName))
            {

            }
        }

        public void DeleteAccountById(Int32 id)
        {
            using (XinJishu.Data.SQLServer.DataAccess conn = new Data.SQLServer.DataAccess(connectionStringName))
            {

            }

        }
        public void DeleteAccountByPublicId(Guid id)
        {
            using (XinJishu.Data.SQLServer.DataAccess conn = new Data.SQLServer.DataAccess(connectionStringName))
            {

            }

        }
        public void UpdateAccount(AccountModel account)
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
