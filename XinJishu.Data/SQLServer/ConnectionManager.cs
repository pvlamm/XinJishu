using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XinJishu.Data.SQLServer
{
    public abstract class ConnectionManager : IDisposable
    {
        protected SqlConnection conn { get; set; }
        private SqlTransaction trans { get; set; }
        private bool useTransactionMode { get; set; }
        private string connection_string { get; set; }
        public ConnectionManager(String connection_string) {

            if (String.IsNullOrWhiteSpace(connection_string))
                throw new Exception("Connection String not optional, it is required");

            this.connection_string = connection_string;
            this.useTransactionMode = false;
            this.trans = null;
            try
            {
                this.conn = new SqlConnection(this.connection_string);
            }
            catch
            {
                var connStr = ConfigurationManager.ConnectionStrings[this.connection_string].ConnectionString;
                this.conn = new SqlConnection(connStr);
                this.connection_string = connStr;
            }
        }
        public SqlCommand GetCommand(){

            if (this.conn.State == System.Data.ConnectionState.Closed)
                this.conn.Open();

            return this.conn.CreateCommand();

        }
        public bool BeginTransaction()
        {
            this.useTransactionMode = true;
            trans = conn.BeginTransaction();

            return this.trans != null;
        }
        public bool RollbackTransaction()
        {
            this.trans.Rollback();
            this.trans = null;
            return this.trans == null;
        }
        public bool CommitTransaction()
        {
            this.trans.Commit();
            this.trans = null;
            return this.trans == null;
        }

        public DataTable ExecuteDataTable(SqlCommand cmd)
        {
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            sda.Fill(dt);

            return dt;
        }
        public virtual List<Hashtable> ExecuteHash(SqlCommand cmd)
        {
            List<Hashtable> resultSet = new List<Hashtable>();

            try
            {

                this.Open();
                var reader = cmd.ExecuteReader();

                int row_index = 0;
                while (reader.Read())
                {
                    int field_index = 0;
                    var row = new Hashtable();
                    while (field_index <= (reader.FieldCount - 1))
                    {
                        row[reader.GetName(field_index)] = reader[field_index];
                        field_index = field_index + 1;
                    }
                    resultSet.Add(row);
                    row_index = row_index + 1;
                }
            }
            catch (Exception x)
            {
                throw x;
            }
            finally
            {
                this.conn.Close();
            }
            return resultSet;
        }
        public virtual async Task<List<Hashtable>> ExecuteHashAsync(SqlCommand cmd)
        {
            List<Hashtable> resultSet = new List<Hashtable>();

            try
            {

                this.Open();
                var reader = await cmd.ExecuteReaderAsync();

                int row_index = 0;
                while (await reader.ReadAsync())
                {
                    int field_index = 0;
                    var row = new Hashtable();
                    while (field_index <= (reader.FieldCount - 1))
                    {
                        row[reader.GetName(field_index)] = reader[field_index];
                        field_index = field_index + 1;
                    }
                    resultSet.Add(row);
                    row_index = row_index + 1;
                }
            }
            catch (Exception x)
            {
                throw x;
            }
            finally
            {
                this.conn.Close();
            }
            return resultSet;
        }
        public virtual List<Hashtable> ExecuteHash(string Sql, params object[] pars)
        {
            List<Hashtable> resultSet = new List<Hashtable>();
            #region [ Connection Verification ]
            if ((conn == null))
            {
                if (this.conn == null)
                {
                    throw new ApplicationException("Call CreateConnection method before using the connection. Database Name is also blank.");
                }
            }
            #endregion

            try
            {
                var Command = new SqlCommand(String.Format(Sql, pars), this.conn);
                this.conn.Open();
                var reader = Command.ExecuteReader();
                int row_index = 0;

                while (reader.Read())
                {
                    int field_index = 0;
                    var row = new Hashtable();

                    while (field_index <= (reader.FieldCount - 1))
                    {
                        row[reader.GetName(field_index)] = reader[field_index];
                        field_index = field_index + 1;
                    }

                    resultSet.Add(row);
                    row_index = row_index + 1;
                }
            }
            catch (Exception x)
            {
                throw x;
            }
            finally
            {
                this.conn.Close();
            }
            return resultSet;
        }
        public virtual List<Hashtable> ExecuteHash(string sql)
        {
            List<Hashtable> resultSet = new List<Hashtable>();

            try
            {
                IDbCommand command = this.conn.CreateCommand();
                command.CommandText = sql;

                this.conn.Open();
                var reader = command.ExecuteReader();

                int row_index = 0;
                while (reader.Read())
                {
                    int field_index = 0;
                    var row = new Hashtable();
                    while (field_index <= (reader.FieldCount - 1))
                    {
                        row[reader.GetName(field_index)] = reader[field_index];
                        field_index = field_index + 1;
                    }
                    resultSet.Add(row);
                    row_index = row_index + 1;
                }
            }
            catch (Exception x)
            {
                throw x;
            }
            finally
            {
                this.conn.Close();
            }
            return resultSet;
        }
        public virtual List<Hashtable> ExecuteHash(string sql, params string[] pars)
        {
            List<Hashtable> resultSet = new List<Hashtable>();

            try
            {
                IDbCommand command = this.conn.CreateCommand();
                command.CommandText = String.Format(sql, pars);

                this.conn.Open();
                var reader = command.ExecuteReader();

                int row_index = 0;
                while (reader.Read())
                {
                    int field_index = 0;
                    var row = new Hashtable();
                    while (field_index <= (reader.FieldCount - 1))
                    {
                        row[reader.GetName(field_index)] = reader[field_index];
                        field_index = field_index + 1;
                    }
                    resultSet.Add(row);
                    row_index = row_index + 1;
                }
            }
            catch (Exception x)
            {
                throw x;
            }
            finally
            {
                this.conn.Close();
            }
            return resultSet;
        }
        public virtual void Open()
        {
            if (this.conn.State == System.Data.ConnectionState.Closed)
                this.conn.Open();
        }
        public virtual void Close()
        {
            this.conn.Close();
        }
        public void Dispose()
        {
            if (this.trans != null)
                this.RollbackTransaction();

            this.conn.Dispose();
        }
    }
}
