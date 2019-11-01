using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BPMInterfaceDAL;

namespace BPMInterfaceBLL
{
    public class ReportDemoBLL
    {
        ReportDemoDAL rdd = new ReportDemoDAL();

        public DataTable ReportDemo(string vendorname)
        {
            return rdd.ReportDemo(vendorname);
        }
    }
}
