using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BPMInterfaceToolkit;
using System.Threading;
using BPMInterfaceBLL;


namespace ImportVendorPaymentPeriodData
{
    public partial class ImportDataTool : Form
    {
        Thread t_readExcel;
        public ImportDataTool()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void llbExportModel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel文件(*.xlsx)|*.xlsx";
            DialogResult result = sfd.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                if (OutputFileHelper.ExportExcelModel(sfd.FileName, "供应商账龄数据模板"))
                {
                    MessageBox.Show("导出成功！");
                }
                else
                {
                    MessageBox.Show("导出失败！");
                }
                sfd.Dispose();
                GC.Collect();
            }
        }

        private void llbSelection_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Excel文件(*.XLSX)|*.XLSX|Excel文件(*.XLS)|*.XLS";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                tbxFilePath.Text = ofd.FileName;
            }
        }

        private void btnInputData_Click(object sender, EventArgs e)
        {
            if (tbxFilePath.Text.Trim() == "")
            {
                MessageBox.Show("请先选择账龄数据文件!");
            }            
            else
            {
                t_readExcel = new Thread(new ThreadStart(ReadExcelModel));
                t_readExcel.Start();
            }
        }

        private void ReadExcelModel()
        {
            lbStatus.Text = "正在读取模板中的数据....";
            DataTable dt_data = ReadFileHelper.ReadExcelFile(tbxFilePath.Text.Trim(), "供应商账龄数据");
            if (dt_data.Rows.Count != 0)
            {
                lbStatus.Text = "正在检查模板数据的正确性......";
                //检查模板
                #region 检查数据列
                Dictionary<string, int> dict = new Dictionary<string, int>();
                dict.Add("cVenCode", 0);
                dict.Add("Less30", 0);
                dict.Add("Less60", 0);
                dict.Add("Less90", 0);
                dict.Add("Less120", 0);
                dict.Add("Less180", 0);
                dict.Add("Less365", 0);
                dict.Add("Less730", 0);
                dict.Add("More730", 0);

                foreach (DataColumn dc in dt_data.Columns)
                {
                    switch (dc.ColumnName.ToString().Trim())
                    {
                        case "cVenCode":
                            dict["cVenCode"]++;
                            break;
                        case "Less30":
                            dict["Less30"]++;
                            break;
                        case "Less60":
                            dict["Less60"]++;
                            break;
                        case "Less90":
                            dict["Less90"]++;
                            break;
                        case "Less120":
                            dict["Less120"]++;
                            break;
                        case "Less180":
                            dict["Less180"]++;
                            break;
                        case "Less365":
                            dict["Less365"]++;
                            break;
                        case "Less730":
                            dict["Less730"]++;
                            break;
                        case "More730":
                            dict["More730"]++;
                            break;
                        default:
                            break;
                    }
                }

                foreach (KeyValuePair<string, int> kvp in dict)
                {
                    if (kvp.Value != 1)
                    {
                        MessageBox.Show("模板数据列明不符合要求！");
                        return;
                    }
                }
                #endregion

                #region 检查数据行
                for (int i = 0; i < dt_data.Rows.Count; i++)
                {
                    for (int j = 0; j < dt_data.Columns.Count; j++)
                    {
                        switch (dt_data.Columns[j].ColumnName.Trim())
                        {
                            case "cVenCode":
                                if (dt_data.Rows[i][j].ToString().Trim().Equals(""))
                                {
                                    dt_data.Rows.RemoveAt(i);
                                    i--;
                                }
                                else
                                {
                                    //不允许出现双引号、单引号、等于号等特殊字符
                                    dt_data.Rows[i][j] = dt_data.Rows[i][j].ToString().Trim().Replace("\"", "");
                                    dt_data.Rows[i][j] = dt_data.Rows[i][j].ToString().Trim().Replace("'", "");
                                    dt_data.Rows[i][j] = dt_data.Rows[i][j].ToString().Trim().Replace("=", "");
                                }
                                break;
                            default:
                                double temp = 0;
                                if (!double.TryParse(dt_data.Rows[i][j].ToString().Trim(), out temp))
                                {
                                    dt_data.Rows[i][j] = 0;
                                }
                                break;
                        }
                    }
                }
                #endregion

                lbStatus.Text = "正在更新数据.....";
                if (dt_data.Rows.Count > 0)
                {
                    //写入数据库
                    string msg = GlobalBLL.vendorPaymentPeroidBLL.UpdateVendorPaymentPeroidData(dt_data);
                    if (msg.Trim().Equals("Success"))
                    {
                        MessageBox.Show("数据导入成功！");
                        lbStatus.Text = "数据导入完成！";
                    }
                    else
                    {
                        MessageBox.Show(msg);
                        lbStatus.Text = "数据导入失败！";
                    }
                }
                else
                {
                    MessageBox.Show("没有可导入的数据，请检查模板中的数据！");
                    lbStatus.Text = "未导入任何数据！";
                }
            }
            else
            {
                MessageBox.Show("没有获取到任何数据！");
                lbStatus.Text = "未导入任何数据！";
            }
        }
    }
}
