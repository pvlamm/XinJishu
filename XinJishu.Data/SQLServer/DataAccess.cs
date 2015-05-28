using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XinJishu.Data.SQLServer
{
    public class DataAccess : ConnectionManager
    {
        public DataAccess(String connection_string)
            : base(connection_string)
        {

        }
    }
}
