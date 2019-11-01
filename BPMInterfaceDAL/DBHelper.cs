using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Configuration;
using BPMInterfaceToolkit;
using System.Data.SqlClient;

namespace BPMInterfaceDAL
{
    public class DBHelper:IDisposable
    {
        private static  string dbProviderName = "System.Data.SqlClient";
        private static  string dbConnectionString = "Data Source=soft.ryyj.net;Initial Catalog=ProgramControl;Persist Security Info=True;User ID=ProgramAdmin;Password=PA91338720";
       
        private SqlConnection connection;
        public DBHelper()
        {            
            this.connection = CreateConnection();
        }
        public DBHelper(string connectionString)
        {
            this.connection = CreateConnection(connectionString);
        }
        public static SqlConnection CreateConnection()
        {
            SqlConnection dbconn = new SqlConnection(DBHelper.dbConnectionString);
            return dbconn;
        }
        public static SqlConnection CreateConnection(string connectionString)
        {
            SqlConnection dbconn = new SqlConnection(connectionString);
            return dbconn;
        }

        public SqlCommand GetStoredProcCommond(string storedProcedure)
        {
            SqlCommand dbCommand = connection.CreateCommand();
            dbCommand.CommandText = storedProcedure;
            dbCommand.CommandType = CommandType.StoredProcedure;
            return dbCommand;
        }
        public SqlCommand GetSqlStringCommond(string sqlQuery)
        {
            SqlCommand dbCommand = connection.CreateCommand();
            dbCommand.CommandText = sqlQuery;
            dbCommand.CommandType = CommandType.Text;
            return dbCommand;
        }

        #region 增加参数
        public void AddParameterCollection(SqlCommand cmd, SqlParameterCollection dbParameterCollection)
        {
            foreach (SqlParameter dbParameter in dbParameterCollection)
            {
                cmd.Parameters.Add(dbParameter);
            }
        }
        public void AddParameter(SqlCommand cmd, string parameterName, DbType dbType, int size, object value)
        {
            SqlParameter dbParameter = cmd.CreateParameter();
            dbParameter.DbType = dbType;
            dbParameter.ParameterName = parameterName;
            dbParameter.Size = size;
            dbParameter.Value = value;
            cmd.Parameters.Add(dbParameter);
        }
        public void AddParameter(SqlCommand cmd, string parameterName, DbType dbType,  object value)
        {
            SqlParameter dbParameter = cmd.CreateParameter();
            dbParameter.DbType = dbType;
            dbParameter.ParameterName = parameterName;
            dbParameter.Value = value;
            cmd.Parameters.Add(dbParameter);
        }
        public void AddParameter(SqlCommand cmd, string parameterName, object value)
        {
            SqlParameter dbParameter = cmd.CreateParameter();
            dbParameter.DbType = DbType.String;
            dbParameter.ParameterName = parameterName;
            dbParameter.Value = value;
            cmd.Parameters.Add(dbParameter);
        }
        public void AddOutParameter(SqlCommand cmd, string parameterName, DbType dbType, int size)
        {
            SqlParameter dbParameter = cmd.CreateParameter();
            dbParameter.DbType = dbType;
            dbParameter.ParameterName = parameterName;
            dbParameter.Size = size;
            dbParameter.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(dbParameter);
        }
        public void AddInParameter(SqlCommand cmd, string parameterName, DbType dbType, object value)
        {
            SqlParameter dbParameter = cmd.CreateParameter();
            dbParameter.DbType = dbType;
            dbParameter.ParameterName = parameterName;
            dbParameter.Value = value;
            dbParameter.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(dbParameter);
        }
        public void AddReturnParameter(SqlCommand cmd, string parameterName, DbType dbType)
        {
            SqlParameter dbParameter = cmd.CreateParameter();
            dbParameter.DbType = dbType;
            dbParameter.ParameterName = parameterName;
            dbParameter.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(dbParameter);
        }
        public SqlParameter GetParameter(SqlCommand cmd, string parameterName)
        {
            return cmd.Parameters[parameterName];
        }

        #endregion

        #region 执行
        public DataSet ExecuteDataSet(SqlCommand cmd)
        {
            SqlDataAdapter dbDataAdapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            dbDataAdapter.Fill(ds);
            return ds;
        }

        public DataTable ExecuteDataTable(SqlCommand cmd)
        {
            SqlDataAdapter dbDataAdapter = new SqlDataAdapter(cmd);
            DataTable dataTable = new DataTable();
            dbDataAdapter.Fill(dataTable);
            return dataTable;
        }

        public SqlDataReader ExecuteReader(SqlCommand cmd)
        {
            cmd.Connection.Open();
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);            
            return reader;
        }
        public int ExecuteNonQuery(SqlCommand cmd)
        {
            cmd.Connection.Open();
            int ret = cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            return ret;
        }

        public object ExecuteScalar(SqlCommand cmd)
        {
            cmd.Connection.Open();
            object ret = cmd.ExecuteScalar();
            cmd.Connection.Close();
            
            return ret;
        }
        #endregion        

        #region 执行事务
        public DataSet ExecuteDataSet(SqlCommand cmd,Trans t)
        {
            cmd.Connection = t.DbConnection;
            cmd.Transaction = t.DbTrans;
            SqlDataAdapter dbDataAdapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            dbDataAdapter.Fill(ds);
            return ds;
        }

        public DataTable ExecuteDataTable(SqlCommand cmd, Trans t)
        {
            cmd.Connection = t.DbConnection;
            cmd.Transaction = t.DbTrans;
            SqlDataAdapter dbDataAdapter = new SqlDataAdapter(cmd);
            DataTable dataTable = new DataTable();
            dbDataAdapter.Fill(dataTable);
            return dataTable;
        }

        public SqlDataReader ExecuteReader(SqlCommand cmd, Trans t)
        {
            cmd.Connection.Close();
            cmd.Connection = t.DbConnection;
            cmd.Transaction = t.DbTrans;            
            SqlDataReader reader = cmd.ExecuteReader();    
            return reader;
        }
        public int ExecuteNonQuery(SqlCommand cmd, Trans t)
        {
            cmd.Connection.Close();
            cmd.Connection = t.DbConnection;
            cmd.Transaction = t.DbTrans;  
            int ret = cmd.ExecuteNonQuery();            
            return ret;
        }

        public object ExecuteScalar(SqlCommand cmd, Trans t)
        {
            cmd.Connection.Close();
            cmd.Connection = t.DbConnection;
            cmd.Transaction = t.DbTrans;  
            object ret = cmd.ExecuteScalar();            
            return ret;
        }
        #endregion


        #region IDisposable 成员

        void IDisposable.Dispose()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion

        /// <summary>
        /// 关闭链接
        /// </summary>
        /// <param name="cmd"></param>
        public void CheckConnection(SqlCommand cmd) 
        {
            if (cmd != null && cmd.Connection.State != ConnectionState.Closed) 
            {
                cmd.Connection.Close();
                cmd.Dispose();
                GC.Collect();
            }
        }

        /// <summary>
        /// 测试连接
        /// </summary>
        /// <returns></returns>
        public string TestConnection() 
        {
            string result = "";
            try
            {
                if (this.connection.State != ConnectionState.Closed)
                {
                    this.connection.Close();                    
                }
                this.connection.Open();
                if (this.connection.State == ConnectionState.Open)
                {
                    result = "连接成功！";
                }
                else
                {
                    result = "连接失败！";
                }
                //MessageBox.Show(this.connection.State.ToString());

            }
            catch (Exception ex)
            {
                result = "连接失败！" + ex.Message;
            }
            finally
            {
                this.connection.Close();
                this.connection.Dispose();
            }
            return result;
        }
    }

    public class Trans : IDisposable
    {
        private SqlConnection conn;
        private SqlTransaction dbTrans;
        public SqlConnection DbConnection
        {
            get { return this.conn; }
        }
        public SqlTransaction DbTrans
        {
            get { return this.dbTrans; }
        }

        public Trans()
        {
            conn = DBHelper.CreateConnection();
            conn.Open();
            dbTrans = conn.BeginTransaction();
        }
        public Trans(string connectionString)
        {
            conn = DBHelper.CreateConnection(connectionString);
            conn.Open();
            dbTrans = conn.BeginTransaction();
        }
        public void Commit()
        {
            dbTrans.Commit();
            this.Close();
        }

        public void RollBack()
        {
            dbTrans.Rollback();
            this.Close();
        }

        public void Dispose()
        {
            this.Close();
        }

        public void Close()
        {
            if (conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
            }
        }
    }
}
