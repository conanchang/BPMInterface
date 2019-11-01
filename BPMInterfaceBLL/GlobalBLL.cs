using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BPMInterfaceBLL
{
    public class GlobalBLL
    {
        /// <summary>
        /// PaymentInfoBLL
        /// </summary>
        public static PaymentInfoBLL paymentInfoBLL = new PaymentInfoBLL();
        /// <summary>
        /// VendorPaymentPeroidBLL
        /// </summary>
        public static VendorBLL vendorPaymentPeroidBLL = new VendorBLL();

        public static ReportDemoBLL reportDemoBLL = new ReportDemoBLL();
    }
}
