using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace BPMInterfaceDAL
{
    
    public class ReportDemoDAL
    {
        SqlCommand cmd;


        public DataTable ReportDemo(string vendorname)
        {
            string sql = string.Format(@"select formSerialNumber as [付款单单号], txtgys as [供应商], txthtmc as [合同名称], txtfkje as [付款金额],fkstate as [付款状态] from FKD");
             if (!vendorname.Trim().Equals(""))
            {
                sql += " where txtgys like N'%" + vendorname.Trim() + "%'";
            }
            DBHelper db = new DBHelper(DBConnectionStrings.dbConnectionString_BPM);
            cmd = db.GetSqlStringCommond(sql);
            return DBExecute.DBExecute_DateTable(db, cmd);
        }

    }
}
