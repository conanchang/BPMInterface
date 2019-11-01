using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using BPMInterfaceModel;
using BPMInterfaceToolkit;
using System.Data;

namespace BPMInterfaceDAL
{
    public class PaymentInfoDAL
    {
        SqlCommand cmd;
        public string CreatePaymentInfo(PaymentInfoModel pi)
        {
            string return_msg = "";
            DBHelper db = new DBHelper(DBConnectionStrings.dbConnectionString_U8);
            string sql = "";
            using (Trans t = new Trans(DBConnectionStrings.dbConnectionString_U8))
            {
                try
                {
                    sql = string.Format(@"select count(cBillID) from NB_payment where cBillID = @cbillid", pi.cBillID.Trim());
                    cmd = db.GetSqlStringCommond(sql);
                    if (Convert.ToInt32(DBExecute.DBExecute_Scalar(db, cmd, t)) != 0)
                    {
                        return_msg = "Error!BPM单据重复提交，单号：" + pi.cBillID.Trim();
                        t.RollBack();
                    }
                    else
                    {
                        sql = string.Format("select Cname,Cvalue from BPMField where Cname in ('BPM_Payment_iID','BPM_Payment_cPackageID')");
                        cmd = db.GetSqlStringCommond(sql);
                        DataTable dt = DBExecute.DBExecute_DateTable(db, cmd, t);
                        int iid = 0;
                        //int cbillid = 0;
                        int cpackageid = 0;
                        if (dt.Rows.Count != 2)
                        {
                            return_msg = "Error!获取支付单相关参数出错！";
                            t.RollBack();
                        }
                        else
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                switch (dr["Cname"].ToString().Trim())
                                {
                                    case "BPM_Payment_iID":
                                        iid = Convert.ToInt32(dr["Cvalue"]) + 1;
                                        break;
                                    case "BPM_Payment_cPackageID":
                                        cpackageid = Convert.ToInt32(dr["Cvalue"]) + 1;
                                        break;
                                    default:
                                        break;
                                }
                            }
                            pi.iID = iid.ToString().Trim();
                            //pi.cBillID = string.Format("BPM{0}", cbillid.ToString("d7"));
                            pi.cPackageId = cpackageid.ToString();

                            #region 生成支付单
                            sql = string.Format(@"insert into NB_payment (iID,cBillID,cBillIdInput,dBillDate,mOriginMoney,cPaySpeed,sExchRate,mNativeMoney,cMoneyType,cAccountFrom,
                                                              cAccountTo,cSettle,cComeFrom,cPurpose,cAppend,cRemark,cComment,cExaminer,cApprover,iExamPass,
                                                              iApprovePass,bPayed,bBalanced,bMakeBill,cOperator,cModifier,cPayer,cDepCode,cItemClass,cItemCode,
                                                              cPerson,cPackageId,iBillStatus,cChargeAcc,cErrInfoRet,cAccountAgent,cItemCodeName,vt_id,cAlterReason,bInner,
                                                              cwfapprover,iwfapprovepass,ireturncount,iswfcontrolled,iflowstate)
                                                    values 
                                                             (@iid,@cbillid,@cbillidinput,@dbilldate,@moriginmoney,@cpayspeed,@sexchrate,@mnativemoney,@cmoneytype,@caccountfrom,
                                                              @caccountto,@csettle,@ccomefrom,@cpurpose,@cappend,@cremark,@ccomment,@cexaminer,@capprover,@iexampass,
                                                              @iapprovepass,@bpayed,@bbalanced,@bmakebill,@coperator,@cmodifier,@cpayer,@cdepcode,@citemclass,@citemcode,
                                                              @cperson,@cpackageid,@ibillstatus,@cchargeacc,@cerrinforet,@caccountagent,@citemcodename,@vtid,@calterreason,@binner,
                                                              @cwfapprover,@iwfapprovepass,@ireturncount,@iswfcontrolled,@iflowstate)");
                            cmd = db.GetSqlStringCommond(sql);
                            db.AddParameter(cmd, "@iid", pi.iID);
                            db.AddParameter(cmd, "@cbillid", pi.cBillID);
                            db.AddParameter(cmd, "@cbillidinput", pi.cBillIdInput);
                            db.AddParameter(cmd, "@dbilldate", pi.dBillDate);
                            db.AddParameter(cmd, "@moriginmoney", pi.mOriginMoney);
                            db.AddParameter(cmd, "@cpayspeed", pi.cPaySpeed);
                            db.AddParameter(cmd, "@sexchrate", pi.sExchRate);
                            db.AddParameter(cmd, "@mnativemoney", pi.mNativeMoney);
                            db.AddParameter(cmd, "@cmoneytype", pi.cMoneyType);
                            db.AddParameter(cmd, "@caccountfrom", pi.cAccountFrom);
                            db.AddParameter(cmd, "@caccountto", pi.cAccountTo);
                            db.AddParameter(cmd, "@csettle", pi.cSettle);
                            db.AddParameter(cmd, "@ccomefrom", pi.cComeFrom);
                            db.AddParameter(cmd, "@cpurpose", pi.cPurpose);
                            db.AddParameter(cmd, "@cappend", pi.cAppend);
                            db.AddParameter(cmd, "@cremark", pi.cRemark);
                            db.AddParameter(cmd, "@ccomment", pi.cComment);
                            db.AddParameter(cmd, "@cexaminer", pi.cExaminer);
                            db.AddParameter(cmd, "@capprover", pi.cApprover);
                            db.AddParameter(cmd, "@iexampass", pi.iExamPass);
                            db.AddParameter(cmd, "@iapprovepass", pi.iApprovePass);
                            db.AddParameter(cmd, "@bpayed", pi.bPayed);
                            db.AddParameter(cmd, "@bbalanced", pi.bBalanced);
                            db.AddParameter(cmd, "@bmakebill", pi.bMakeBill);
                            db.AddParameter(cmd, "@coperator", pi.cOperator);
                            db.AddParameter(cmd, "@cmodifier", pi.cModifier);
                            db.AddParameter(cmd, "@cpayer", pi.cPayer);
                            db.AddParameter(cmd, "@cdepcode", pi.cDepCode);
                            db.AddParameter(cmd, "@citemclass", pi.cItemClass);
                            db.AddParameter(cmd, "@citemcode", pi.cItemCode);
                            db.AddParameter(cmd, "@cperson", pi.cPerson);
                            db.AddParameter(cmd, "@cpackageid", pi.cPackageId);
                            db.AddParameter(cmd, "@ibillstatus", pi.iBillStatus);
                            db.AddParameter(cmd, "@cchargeacc", pi.cChargeAcc);
                            db.AddParameter(cmd, "@cerrinforet", pi.cErrInfoRet);
                            db.AddParameter(cmd, "@caccountagent", pi.cAccountAgent);
                            db.AddParameter(cmd, "@citemcodename", pi.cItemCodeName);
                            db.AddParameter(cmd, "@vtid", pi.vt_id);
                            db.AddParameter(cmd, "@calterreason", pi.cAlterReason);
                            db.AddParameter(cmd, "@binner", pi.bInner);
                            db.AddParameter(cmd, "@cwfapprover", pi.cwfapprover);
                            db.AddParameter(cmd, "@iwfapprovepass", pi.iwfapprovepass);
                            db.AddParameter(cmd, "@ireturncount", pi.ireturncount);
                            db.AddParameter(cmd, "@iswfcontrolled", pi.iswfcontrolled);
                            db.AddParameter(cmd, "@iflowstate", pi.iflowstate);
                            DBExecute.DBExecute_NonQuery(db, cmd, t);
                            #endregion

                            #region  生成插入日志
                            sql = string.Format(@" insert into NB_OperateLog (iId,cBillId,mMoney,cAccountFrom,cAccountTo,cOperatorName,cOperateType,dLogDate,cPackageId)
                                                   values 
                                                (@iid,@cbillid,@mmoney,@caccountfrom,@caccountto,@coperatorname,@coperatetype,GETDATE(),@cpackageid)");
                            cmd = db.GetSqlStringCommond(sql);
                            db.AddParameter(cmd, "@iid", pi.iID);
                            db.AddParameter(cmd, "@cbillid", pi.cBillID);
                            db.AddParameter(cmd, "@mmoney", pi.mNativeMoney);
                            db.AddParameter(cmd, "@caccountfrom", pi.cAccountFrom);
                            db.AddParameter(cmd, "@caccountto", pi.cAccountTo);
                            db.AddParameter(cmd, "@coperatorname", pi.cOperator);
                            db.AddParameter(cmd, "@coperatetype", pi.cOperatType);
                            db.AddParameter(cmd, "@cpackageid", pi.cPackageId);
                            DBExecute.DBExecute_NonQuery(db, cmd, t);
                            #endregion

                            #region 更新ID表
                            sql = string.Format(@"update BPMField set Cvalue = @bpmiid where Cname = 'BPM_Payment_iID'
                                              update BPMField set Cvalue = @bpmcpackageid where Cname = 'BPM_Payment_cPackageID'");
                            cmd = db.GetSqlStringCommond(sql);
                            db.AddParameter(cmd, "@bpmiid", iid);
                            db.AddParameter(cmd, "@bpmcpackageid", cpackageid);
                            DBExecute.DBExecute_NonQuery(db, cmd, t);
                            #endregion

                            return_msg = "success";
                            t.Commit();
                        }
                    }
                    
                }
                catch (Exception e)
                {
                    return_msg = "Error!" + e.Message;
                    Log.WriteLog(string.Format(@"BPM单号：{0}生成支付单出错，出错信息：{1}", pi.cBillIdInput, e.Message));
                    t.RollBack();
                }
                finally
                {
                    db.CheckConnection(cmd);
                }
            }
            return return_msg;

        }
    }
}
