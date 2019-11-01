using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BPMInterfaceModel
{
    public class VendorInfoModel
    {
        /// <summary>
        /// 供应商编码
        /// </summary>
        public string cVenCode;
        /// <summary>
        /// 供应商名称
        /// </summary>
        public string cVenName;
        /// <summary>
        /// 供应商简称
        /// </summary>
        public string cVenAbbName;
        /// <summary>
        /// 供应商是否国外, 中国 0  国外 1
        /// </summary>
        public int bVenOverseas;
        /// <summary>
        /// 供应商分类编码
        /// </summary>
        public string cVCCode;
        /// <summary>
        /// 所属地区编码
        /// </summary>
        public string cDCCode;
        /// <summary>
        /// 开户银行
        /// </summary>
        public string cVenBank;
        /// <summary>
        /// 开户银行银行账号
        /// </summary>
        public string cVenAccount;
        /// <summary>
        /// 税率
        /// </summary>
        public float iVenTaxRate;
        /// <summary>
        /// 币种
        /// </summary>
        public string cVenExch_name;
        /// <summary>
        /// 所属银行代码
        /// </summary>
        public string cVenBankCode;
        /// <summary>
        /// 纳税人登记号， 税号
        /// </summary>
        public string cVenRegCode;
        /// <summary>
        /// 备注
        /// </summary>
        public string cMemo;
        /// <summary>
        /// 单价是否含税 默认为0
        /// </summary>
        public int bVenTax = 0;
        /// <summary>
        /// 营业执照是否期限管理 默认为0
        /// </summary>
        public int bLicenceDate = 0;
        /// <summary>
        /// 经营许可证是否期限管理  默认为0
        /// </summary>
        public int bBusinessDate = 0;
        /// <summary>
        /// 法人委托书是否期限管理 默认为0
        /// </summary>
        public int bProxyDate = 0;
        /// <summary>
        /// 是否通过GMP认证   默认为0
        /// </summary>
        public int bPassGMP = 0;
        /// <summary>
        /// 是否货物 默认为1
        /// </summary>
        public int bVenCargo = 1;
        /// <summary>
        /// 是否委外  默认为0
        /// </summary>
        public int bProxyForeign = 0;
        /// <summary>
        /// 是否服务 默认为0
        /// </summary>
        public int bVenService = 0;
        /// <summary>
        /// 企业类型  默认为0
        /// </summary>
        public int iVenGSPType = 0;
        /// <summary>
        /// 账期管理  默认为0
        /// </summary>
        public int bVenAccPeriodMng = 0;
        /// <summary>
        /// 是否有分支机构 默认为0
        /// </summary>
        public int bVenHomeBranch = 0;
        /// <summary>
        /// </summary>
        public DateTime dVenCreateDatetime;


    }
}
