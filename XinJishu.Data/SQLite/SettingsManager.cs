using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

using XinJishu.Data.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace XinJishu.Data.SQLite
{
    public class SettingsManager : ConnectionManager, ISettings
    {
        public SettingsManager(String connection_string) : base(connection_string) {

            InitializeTable();

        }


        public T GetValue<T>(string Key)
        {
            using (SQLiteCommand sql_comm = this.GetCommand())
            {

                sql_comm.CommandType = System.Data.CommandType.Text;
                sql_comm.CommandText = @"SELECT Value FROM Settings WHERE Key = @Key";
                sql_comm.Parameters.Clear();
                sql_comm.Parameters.AddWithValue("@Key", Key);

                SQLiteDataAdapter sda = new SQLiteDataAdapter(sql_comm);
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

        public Int32 SetValue<T>(string Key, object Value)
        {                
            using (SQLiteCommand sql_comm = this.GetCommand())
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

        public Boolean KeyExists(String Key)
        {

            using (SQLiteCommand sql_comm = this.GetCommand())
            {

                sql_comm.CommandType = System.Data.CommandType.Text;
                sql_comm.CommandText = @"SELECT COUNT(*) AS Count FROM Settings WHERE Key = @Key";
                sql_comm.Parameters.Clear();
                sql_comm.Parameters.AddWithValue("@Key", Key);

                SQLiteDataAdapter sda = new SQLiteDataAdapter(sql_comm);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                
                Int32 Count = 0;

                if( dt.Rows.Count == 1)
                    Count = Convert.ToInt32( dt.Rows[0]["Count"] );

                if (Count == 1)
                    return true;

                return false;

            }
        }

        private void InitializeTable(){

            String sql = @"CREATE TABLE IF NOT EXISTS
    [Settings] (
        [Key] VARCHAR(25)  UNIQUE NOT NULL PRIMARY KEY,
        [Value] NVARCHAR(5120)  NULL
    )
    ";
            using (SQLiteCommand sql_comm = GetCommand())
            {
                sql_comm.CommandType = CommandType.Text;
                sql_comm.CommandText = sql;
                sql_comm.ExecuteNonQuery();
            }

        }

    }
}
