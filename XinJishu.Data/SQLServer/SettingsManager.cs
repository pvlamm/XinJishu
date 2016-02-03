using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XinJishu.Data.Interfaces;

namespace XinJishu.Data.SQLServer
{
    public class SettingsManager : ConnectionManager, ISettings
    {
        public SettingsManager(string connection_string) : base(connection_string) {

            InitializeTable();

        }
        public T GetValue<T>(string Key)
        {
            using (SqlCommand sql_comm = this.GetCommand())
            {

                sql_comm.CommandType = System.Data.CommandType.Text;
                sql_comm.CommandText = @"SELECT Value FROM Settings WHERE Key = @Key";
                sql_comm.Parameters.Clear();
                sql_comm.Parameters.AddWithValue("@Key", Key);

                SqlDataAdapter sda = new SqlDataAdapter(sql_comm);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                if (dt.Rows.Count == 1)
                {
                    return (T)dt.Rows[0]["Value"];
                }
                else
                    return default(T);
            }
        }

        public int SetValue<T>(string Key, object Value)
        {
            using (SqlCommand sql_comm = this.GetCommand())
            {

                String val = String.Empty;

                if (typeof(T) == typeof(byte[]))
                    val = System.Text.Encoding.Default.GetString((byte[])Value);
                else
                    val = Value.ToString();

                sql_comm.CommandType = System.Data.CommandType.Text;
                sql_comm.CommandText = @"INSERT OR REPLACE INTO Settings (Key, Value) VALUES (@Key, @Value) ";
                sql_comm.Parameters.Clear();
                sql_comm.Parameters.AddWithValue("@Key", Key);
                sql_comm.Parameters.AddWithValue("@Value", val);

                return sql_comm.ExecuteNonQuery();

            }
        }

        public bool KeyExists(string Key)
        {

            using (SqlCommand sql_comm = this.GetCommand())
            {

                sql_comm.CommandType = System.Data.CommandType.Text;
                sql_comm.CommandText = @"SELECT COUNT(*) AS Count FROM Settings WHERE Key = @Key";
                sql_comm.Parameters.Clear();
                sql_comm.Parameters.AddWithValue("@Key", Key);

                SqlDataAdapter sda = new SqlDataAdapter(sql_comm);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                Int32 Count = 0;

                if (dt.Rows.Count == 1)
                    Count = Convert.ToInt32(dt.Rows[0]["Count"]);

                return Count == 1;

            }
        }

        private void InitializeTable()
        {

            String sql = @"
IF NOT EXISTS (select * from sysobjects where name='Settings' and xtype='U')
    CREATE TABLE Settings (
        [Key] VARCHAR(25) UNIQUE NOT NULL PRIMARY KEY,
        [Value] NVARCHAR(5120) NULL
    )
GO";
            using (SqlCommand cmd = GetCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
            }

        }
    }
}
