using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XinJishu.Games.Empires.Data
{
    public class DataSource : XinJishu.Data.SQLServer.ConnectionManager
    {
        private Int32 galaxy_id { get; set; }


        public DataSource(String connection_string)
            : base(connection_string)
        {

        }

        public 
    }
}
