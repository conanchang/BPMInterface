using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BPMInterfaceDAL;
using System.Data;

namespace BPMInterfaceBLL
{
    public class VendorBLL
    {
        VendorDAL vppd = new VendorDAL();

        public string UpdateVendorPaymentPeroidData(DataTable dt)
        {
            return vppd.UpdateVendorPaymentPeroidData(dt);
        }
    }
}
