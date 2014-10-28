using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;


namespace XinJishu.Data.SQLite
{
    public abstract class ConnectionManager : IDisposable
    {
        private SQLiteConnection sql_conn { get; set; }
        private String connection_string { get; set; }

        public ConnectionManager(String connection_string)
        {
            if (String.IsNullOrEmpty(connection_string))
                this.connection_string = connection_string;
            else
                this.connection_string = "Data Source=sqlite.db;Version=3;Legacy Format=True;";

            sql_conn = new SQLiteConnection(this.connection_string);

        }

        public SQLiteCommand GetCommand()
        {
            if (sql_conn.State != System.Data.ConnectionState.Open)
                sql_conn.Open();

            return sql_conn.CreateCommand();
        }


        public void Dispose()
        {
            sql_conn.Dispose();
        }
    }
}
