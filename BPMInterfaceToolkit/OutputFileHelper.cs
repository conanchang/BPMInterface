using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infragistics.Documents.Excel;

namespace BPMInterfaceToolkit
{
    public class OutputFileHelper
    {
        public static bool ExportExcelModel(string filepath, string type)
        {
            bool _b = false;
            Infragistics.Documents.Excel.Workbook wb = new Workbook(WorkbookFormat.Excel2007);
            try
            {
                //Infragistics.Documents.Excel.Workbook wb = new Workbook(); //2003格式
                switch (type.Trim())
                {
                    case "供应商账龄数据模板":
                        wb.Worksheets.Add("VendorPaymentPeriod"); 
                        wb.Worksheets[0].Rows[0].Cells[0].Value = "cVenCode";
                        wb.Worksheets[0].Rows[0].Cells[1].Value = "Less30";
                        wb.Worksheets[0].Rows[0].Cells[2].Value = "Less60";
                        wb.Worksheets[0].Rows[0].Cells[3].Value = "Less90";
                        wb.Worksheets[0].Rows[0].Cells[4].Value = "Less120";
                        wb.Worksheets[0].Rows[0].Cells[5].Value = "Less180";
                        wb.Worksheets[0].Rows[0].Cells[6].Value = "Less365";
                        wb.Worksheets[0].Rows[0].Cells[7].Value = "Less730";
                        wb.Worksheets[0].Rows[0].Cells[8].Value = "More730";
                        break;
                    default:
                        break;
                }               
                //for (int i = 0; i < 5; i++)
                //{
                //    wb.Worksheets[0].Columns[i].Width = 3000;
                //}

                wb.Save(filepath);
                _b = true;           

            }
            catch (Exception e)
            {
                Log.WriteLog(e.Message);
            }
            finally
            {
                GC.Collect();
            }
            return _b;
        }



    }
}
