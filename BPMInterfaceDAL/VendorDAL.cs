using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using BPMInterfaceToolkit;

namespace BPMInterfaceDAL
{
    public class VendorDAL
    {
        SqlCommand cmd;
        public string UpdateVendorPaymentPeroidData(DataTable dt)
        {
            string return_msg = "";
            string strSql = "";
            DBHelper db = new DBHelper(DBConnectionStrings.dbConnectionString_BPM);
            using (Trans t = new Trans(DBConnectionStrings.dbConnectionString_BPM))
            {
                try
                {
                    //获取最新的ID
                    strSql = string.Format(@"select Cvalue from CustomConfig where Cname = N'VendorPaymentPeroidID'");
                    cmd = db.GetSqlStringCommond(strSql);
                    int newID = Convert.ToInt32(DBExecute.DBExecute_Scalar(db, cmd, t)) + 1;
                    int currentCount = 0;
                    strSql = "";
                    //拼接插入sql语句
                    foreach (DataRow dr in dt.Rows)
                    {
                        strSql += string.Format(@"insert into [VendorPaymentPeroidDetail] values ('{0}',{1},{2},{3},{4},{5},{6},{7},{8},{9},getdate()) ",
                                                  dr["cVenCode"].ToString().Trim(), newID.ToString().Trim(), dr["Less30"].ToString().Trim(),
                                                   dr["Less60"].ToString().Trim(), dr["Less90"].ToString().Trim(), dr["Less120"].ToString().Trim(),
                                                    dr["Less180"].ToString().Trim(), dr["Less365"].ToString().Trim(), dr["Less730"].ToString().Trim(),
                                                     dr["More730"].ToString().Trim());
                        currentCount++;
                        if (currentCount == 999)
                        {
                            cmd = db.GetSqlStringCommond(strSql);
                            DBExecute.DBExecute_NonQuery(db, cmd, t);
                            currentCount = 0;
                            strSql = "";
                        }
                    }
                    cmd = db.GetSqlStringCommond(strSql);
                    DBExecute.DBExecute_NonQuery(db, cmd, t);
                    //更新ID
                    strSql = string.Format(@"update CustomConfig set Cvalue = {0} where Cname = N'VendorPaymentPeroidID'", newID.ToString().Trim());
                    cmd = db.GetSqlStringCommond(strSql);
                    DBExecute.DBExecute_NonQuery(db, cmd, t);
                    return_msg = "Success";
                    t.Commit();
                }
                catch (Exception e)
                {
                    return_msg = "Error," + e.Message;
                    Log.WriteLog(e.Message);
                    t.RollBack();
                }
                finally
                {
                    db.CheckConnection(cmd);
                }
            }
            return return_msg;
        }
    }
}
