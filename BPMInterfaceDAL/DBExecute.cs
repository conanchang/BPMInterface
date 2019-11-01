using System;
using System.Data;
using System.Data.SqlClient;

namespace BPMInterfaceDAL
{
    public class DBExecute
    {
        public static DataTable DBExecute_DateTable(DBHelper db, SqlCommand cmd)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = db.ExecuteDataTable(cmd);
            }
            catch (Exception ex)
            {
                BPMInterfaceToolkit.Log.WriteLog(ex.Message.Trim());
            }
            finally
            {
                db.CheckConnection(cmd);
            }
            return dt;
        }

        public static object DBExecute_Scalar(DBHelper db, SqlCommand cmd)
        {
            object o = new object();
            try
            {
                o = db.ExecuteScalar(cmd);
            }
            catch (Exception ex)
            {
                BPMInterfaceToolkit.Log.WriteLog(ex.Message.Trim());
            }
            finally
            {
                db.CheckConnection(cmd);
            }
            return o;
        }

        public static object DBExecute_NonQuery(DBHelper db, SqlCommand cmd)
        {
            int count = 0;
            try
            {
                count = db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                BPMInterfaceToolkit.Log.WriteLog(ex.Message.Trim());
            }
            finally
            {
                db.CheckConnection(cmd);
            }
            return count;
        }

        public static DataTable DBExecute_DateTable(DBHelper db, SqlCommand cmd, Trans t)
        {
            DataTable dt = db.ExecuteDataTable(cmd, t);
            return dt;
        }

        public static object DBExecute_Scalar(DBHelper db, SqlCommand cmd, Trans t)
        {
            object o = db.ExecuteScalar(cmd, t);
            return o;
        }

        public static int DBExecute_NonQuery(DBHelper db, SqlCommand cmd, Trans t)
        {
            int count = db.ExecuteNonQuery(cmd, t);
            return count;
        }



    }
}
