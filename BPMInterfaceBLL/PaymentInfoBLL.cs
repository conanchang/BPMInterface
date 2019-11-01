using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BPMInterfaceDAL;
using BPMInterfaceModel;

namespace BPMInterfaceBLL
{
    public class PaymentInfoBLL
    {
        PaymentInfoDAL pid = new PaymentInfoDAL();

        public string CreatePaymentInfo(PaymentInfoModel pi)
        {
            return pid.CreatePaymentInfo(pi);
        }
    }
}
