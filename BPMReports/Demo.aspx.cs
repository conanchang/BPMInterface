using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BPMInterfaceBLL;
using System.Data;

namespace BPMReports
{
    public partial class Demo : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            DataTable dt = GlobalBLL.reportDemoBLL.ReportDemo(tbxVendor.Text.Trim());
            GridView1.DataSource = null;
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
    }
}