using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BPMInterfaceModel;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;

namespace BPMInterfaceToolkit
{
    public class JsonAnalysis
    {
        
        public static object JsonAnalysisFunction(string json, string type)
        {
            object o = null;
            JArray jArray = new JArray();
            try
            {
                //JObject jo = JsonConvert.DeserializeObject(json) as JObject;
                string jsonData = JsonConvert.SerializeObject(json);
                JsonReader jr = new JsonTextReader(new StringReader(jsonData));
                while (jr.Read())
                {
                    jArray = JArray.Parse(jr.Value.ToString());
                    switch (type)
                    {
                        case "PaymentInfo":
                            //PaymentInfoModel[] pis = new PaymentInfoModel[jArray.Count];
                            int pis_currentcount = 0;
                            int pis_length = 1;
                            foreach (var jsonitem in jArray)
                            {
                                //PaymentInfoModel pi = new PaymentInfoModel();
                                JObject job = (JObject)jsonitem;
                                double money = Convert.ToDouble(job["mOriginMoney"]);
                                if (money > 1900000)
                                {
                                    if (money % 1900000 != 0)
                                    {
                                        pis_length = Convert.ToInt32(money / 1900000) + 1;
                                    }
                                    else
                                    {
                                        pis_length = Convert.ToInt32(money / 1900000);
                                    }
                                }
                                PaymentInfoModel[] pis = new PaymentInfoModel[pis_length];

                                while (money > 1900000)
                                {
                                    PaymentInfoModel pi = new PaymentInfoModel();
                                    if (pis_currentcount == 0)
                                    {
                                        pi.cBillID = job["cBillId"].ToString().Trim();
                                    }
                                    else
                                    {
                                        pi.cBillID = job["cBillId"].ToString().Trim() + "-" + pis_currentcount.ToString().Trim();
                                    }                                   
                                    pi.dBillDate = Convert.ToDateTime(job["cBillDate"]);
                                    pi.mOriginMoney = 1900000;
                                    pi.mNativeMoney = 1900000;
                                    pi.cAccountTo = job["cAccountTo"].ToString().Trim();
                                    pi.cPurpose = job["cPurpose"].ToString().Trim();
                                    pi.bInner = Convert.ToInt32(job["bInner"]);
                                    //pi.emailAddress = job["emailAddress"].ToString().Trim();
                                    pi.analysisResult = "success";

                                    pis[pis_currentcount] = pi;
                                    pis_currentcount++;
                                    money = money - 1900000;
                                }

                                if (money != 0)
                                {
                                    PaymentInfoModel pi = new PaymentInfoModel();
                                    if (pis_currentcount == 0)
                                    {
                                        pi.cBillID = job["cBillId"].ToString().Trim();
                                    }
                                    else
                                    {
                                        pi.cBillID = job["cBillId"].ToString().Trim() + "-" + pis_currentcount.ToString().Trim();
                                    }
                                    pi.dBillDate = Convert.ToDateTime(job["cBillDate"]);
                                    pi.mOriginMoney = money;
                                    pi.mNativeMoney = money;
                                    pi.cAccountTo = job["cAccountTo"].ToString().Trim();
                                    pi.cPurpose = job["cPurpose"].ToString().Trim();
                                    pi.bInner = Convert.ToInt32(job["bInner"]);
                                    //pi.emailAddress = job["emailAddress"].ToString().Trim();
                                    pi.analysisResult = "success";
                                    pis[pis_currentcount] = pi;                                    
                                }
                                o = pis;
                            }
                            
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                switch (type)
                {
                    case "PaymentInfo":                       
                        PaymentInfoModel[] pis = new PaymentInfoModel[1];
                        pis[0] = new PaymentInfoModel();
                        pis[0].analysisResult = "error";                           
                        o = pis;
                        Log.WriteLog(string.Format("PaymentInfo: {0}", e.ToString()));  
                        break;
                    default:
                        break;
                }
            }
           

            return o;
        }

    }
}
