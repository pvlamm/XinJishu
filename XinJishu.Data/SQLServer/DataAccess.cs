using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

        public SwitchDatabaseResult ChangeDatabase(string database, bool createDatabase)
        {
            if (this.conn.State == System.Data.ConnectionState.Open)
            {
                try
                {
                    this.conn.ChangeDatabase(database);

                    return SwitchDatabaseResult.Success;
                }
                catch (SqlException s)
                {
                    if (createDatabase)
                    {
                        using (var cmd = this.GetCommand())
                        {
                            cmd.CommandText = "CREATE DATABASE " + database;
                            cmd.CommandType = System.Data.CommandType.Text;

                            cmd.ExecuteNonQuery();

                        }

                        this.conn.ChangeDatabase(database);

                        return SwitchDatabaseResult.SuccessDBCreated;
                    }
                }
            }

            return SwitchDatabaseResult.Failure;
        }
    }

    public enum SwitchDatabaseResult{
        Success,
        SuccessDBCreated,
        Failure
    }
}
