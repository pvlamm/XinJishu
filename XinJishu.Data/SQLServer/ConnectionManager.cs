using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XinJishu.Data.SQLServer
{
    public abstract class ConnectionManager : IDisposable
    {
        private SqlConnection conn { get; set; }
        private string connection_string { get; set; }

        public ConnectionManager(String connection_string) {

            if (String.IsNullOrWhiteSpace(connection_string))
                throw new Exception("Connection String not optional, it is required");

            this.connection_string = connection_string;

            this.conn = new SqlConnection(this.connection_string);

        }

        public SqlCommand GetCommand(){

            if (this.conn.State == System.Data.ConnectionState.Closed)
                this.conn.Open();

            return this.conn.CreateCommand();

        }

        public DataTable ExecuteDataTable(SqlCommand cmd)
        {
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            sda.Fill(dt);

            return dt;
        }

        public void Dispose()
        {
            this.conn.Dispose();
        }

    }
}
