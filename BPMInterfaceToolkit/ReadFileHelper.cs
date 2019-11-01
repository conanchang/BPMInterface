using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Data.OleDb;

namespace BPMInterfaceToolkit
{
    public class ReadFileHelper
    {
        static string strProvider_XLS = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=";
        static string strExt_XLS = "; Extended Properties='Excel 8.0;HDR=Yes;IMEX=1'";
        static string strProvider_XLSX = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=";
        static string strExt_XLSX = "; Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1'";

        #region Kill Special Excel Process
        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);
        #endregion  
        private delegate void InvokeHandler();

        public static DataTable ReadExcelFile(string filepath, string type)
        {
            DataTable resultDT = new DataTable();
            string strConn = "";
            if (System.IO.Path.GetExtension(filepath).ToLower().Equals(".xlsx"))  //如果文件是Excel2007以上格式
            {
                strConn = strProvider_XLSX + filepath + strExt_XLSX;
            }
            else  //如果文件时Excel2003格式
            {
                strConn = strProvider_XLS + filepath + strExt_XLS;
            }
            string strExcel = "";

            switch (type.Trim())
            {
                case "供应商账龄数据":
                    strExcel = string.Format("SELECT * FROM [{0}$]", "VendorPaymentPeriod");
                    break;
                default:
                    break;
            }

            OleDbConnection conn = new OleDbConnection(strConn);            
            try
            {
                conn.Open();
                OleDbDataAdapter myCommand = new OleDbDataAdapter(strExcel, strConn);
                myCommand.Fill(resultDT);
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Dispose();
            }
            return resultDT;
        }
    }
}
