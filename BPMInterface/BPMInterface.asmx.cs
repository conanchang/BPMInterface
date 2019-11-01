using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using BPMInterfaceModel;
using BPMInterfaceToolkit;
using BPMInterfaceBLL;
using System.Web.Services.Protocols;
using System.Web.Services.Description;

namespace BPMInterface
{
    /// <summary>
    /// BPMInterface 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [SoapDocumentService(RoutingStyle = SoapServiceRoutingStyle.RequestElement)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class BPMInterface : System.Web.Services.WebService
    {
        string return_msg = "";

        [SoapRpcMethod(Use = SoapBindingUse.Literal, Action = "http://tempuri.org/CreatePaymentInfo",
         RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/")]

        [WebMethod(Description = "生成付款信息，写入U8数据库")]        
        public string CreatePaymentInfo(string paymentInfo)
        {
            string body = "";
            if (string.IsNullOrEmpty(paymentInfo))
            {
                return "error";
            }

            if (!paymentInfo.Trim().Substring(0, 1).Equals("["))
            {
                paymentInfo = "[" + paymentInfo.Trim() + "]";
            }
            Log.WriteLog(string.Format("{0}接收到信息：{1}", DateTime.Now.ToString(), paymentInfo));
            //PaymentInfoBLL pib = new PaymentInfoBLL();
            
            PaymentInfoModel[] pis = (PaymentInfoModel[])JsonAnalysis.JsonAnalysisFunction(paymentInfo.Trim(), "PaymentInfo");
            foreach (PaymentInfoModel pi in pis)
            {
                switch (pi.analysisResult.Trim())
                {
                    case "success":
                        //return_msg = "success";
                        return_msg = GlobalBLL.paymentInfoBLL.CreatePaymentInfo(pi);
                        //return_msg = pib.CreatePaymentInfo(pi);
                        if (!return_msg.Trim().Equals("success"))
                        {
                            //邮件通知
                            body = EmailHelper.CreateEmailBody("用款申请单", GlobalInfoModel.error_Type_CreateError, pi.cBillIdInput.Trim(), return_msg);
                            EmailHelper.SendMails(GlobalInfoModel.adminEmailsAdd, "", "付款单生成失败", body, "BPM-U8信息提醒");
                            return_msg = "error";
                        }
                        break;
                    case "error":
                        //邮件通知
                        return_msg = pi.analysisResult;
                        body = EmailHelper.CreateEmailBody("用款申请单", GlobalInfoModel.error_Type_JsonAnalysisEroor, paymentInfo.ToString().Trim(), "Json数据解析出错！");
                        EmailHelper.SendMails(GlobalInfoModel.adminEmailsAdd, "", "付款单生成失败", body, "BPM-U8信息提醒");
                        break;
                    default: break;
                }
            }
            return return_msg;
        }

       
    }
}
