using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BPMInterfaceModel
{
    public class PaymentInfoModel
    {
        /// <summary>
        /// 付款单内部ID
        /// </summary>
        public string iID;
        /// <summary>
        /// 票据单号
        /// </summary>
        public string cBillID;
        /// <summary>
        /// 被引入支付单据号
        /// </summary>
        public string cBillIdInput = "";
        /// <summary>
        /// 票据日期
        /// </summary>
        public DateTime dBillDate;
        /// <summary>
        /// 原币金额
        /// </summary>
        public double mOriginMoney;
        /// <summary>
        /// 付款速度:默认普通
        /// </summary>
        public string cPaySpeed = "普通";
        /// <summary>
        /// 汇率，默认1
        /// </summary>
        public float sExchRate = 1;
        /// <summary>
        /// 本币金额
        /// </summary>
        public double mNativeMoney;
        /// <summary>
        /// 币种名称:默认人民币
        /// </summary>
        public string cMoneyType = "人民币";
        /// <summary>
        /// 支付或转出账号,默认
        /// </summary>
        public string cAccountFrom = "310069079018010051160";
        /// <summary>
        /// 收款账号
        /// </summary>
        public string cAccountTo;
        /// <summary>
        /// 结算方式编码:默认204
        /// </summary>
        public string cSettle = "204";
        /// <summary>
        /// 单据来源，默认："Interface"
        /// </summary>
        public string cComeFrom = "Interface";
        /// <summary>
        /// 用途
        /// </summary>
        public string cPurpose;
        /// <summary>
        /// 附言
        /// </summary>
        public string cAppend = "";
        /// <summary>
        /// 备注
        /// </summary>
        public string cRemark = "";
        /// <summary>
        /// 不通过说明
        /// </summary>
        public string cComment = "";
        /// <summary>
        /// 复核人,默认System
        /// </summary>
        public string cExaminer = "System";
        /// <summary>
        /// 审批人
        /// </summary>
        public string cApprover = "";
        /// <summary>
        /// 初始审核状态：已复审2，未复审0   默认2
        /// </summary>
        public int iExamPass = 2;
        /// <summary>
        /// 初始审核状态：默认0
        /// </summary>
        public int iApprovePass = 0;
        /// <summary>
        /// 是否支付:默认0
        /// </summary>
        public int bPayed = 0;
        /// <summary>
        /// 是否已经对帐:默认0
        /// </summary>
        public int bBalanced = 0;
        /// <summary>
        /// 是否已经制单:默认0
        /// </summary>
        public int bMakeBill = 0;
        /// <summary>
        /// 制单人,默认System
        /// </summary>
        public string cOperator = "System";
        /// <summary>
        /// 操作类型，插日志用，默认：复核
        /// </summary>
        public string cOperatType = "复核";
        /// <summary>
        /// 修改人
        /// </summary>
        public string cModifier = "";
        /// <summary>
        /// 支付人
        /// </summary>
        public string cPayer = "";
        /// <summary>
        /// 部门编码
        /// </summary>
        public string cDepCode = "";
        /// <summary>
        /// 项目大类编码
        /// </summary>
        public string cItemClass = "";
        /// <summary>
        /// 项目编码
        /// </summary>
        public string cItemCode = "";
        /// <summary>
        /// 职员编码
        /// </summary>
        public string cPerson = "";
        /// <summary>
        /// 转账线索号
        /// </summary>
        public string cPackageId;
        /// <summary>
        /// 支付状态,默认：0
        /// </summary>
        public int iBillStatus = 0;
        /// <summary>
        /// 手续费账号,默认为空
        /// </summary>
        public string cChargeAcc = "";
        /// <summary>
        /// 银行返回错误
        /// </summary>
        public string cErrInfoRet = "";
        /// <summary>
        /// 代理付款账号
        /// </summary>
        public string cAccountAgent = "";
        /// <summary>
        /// 项目名称
        /// </summary>
        public string cItemCodeName = "";
        /// <summary>
        /// 单据模板号,默认131047
        /// </summary>
        public string vt_id = "131047";
        /// <summary>
        /// 变更原因
        /// </summary>
        public string cAlterReason = "";
        /// <summary>
        /// 是否同城：是1，不是0
        /// </summary>
        public int bInner;

        public string cwfapprover = "";
        public int iwfapprovepass = 0;
        public int ireturncount = 0;
        public int iswfcontrolled = 0;
        public int iflowstate = 0;

        /// <summary>
        /// Json解析结果，成功Success 出错 Error
        /// </summary>
        public string analysisResult = "";
        /// <summary>
        /// 需要通知的电子邮件地址
        /// </summary>
        public string emailAddress;
        /// <summary>
        /// 用于查验是否重复提交的cBillID
        /// </summary>
        public string checkcBillID;
    }
}
