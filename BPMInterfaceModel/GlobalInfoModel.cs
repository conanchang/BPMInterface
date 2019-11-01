using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace BPMInterfaceModel
{
    public class GlobalInfoModel
    {
        /// <summary>
        /// 管理员电子邮件地址
        /// </summary>
        public static string adminEmailsAdd = ConfigurationManager.AppSettings["AdminEmailsAdd"].Trim();
        /// <summary>
        /// 管理员电子邮件地址
        /// </summary>
        public static string financeEmailsAdd = ConfigurationManager.AppSettings["FinanceEmailsAdd"].Trim();
        /// <summary>
        /// 生成单据失败返回值
        /// </summary>
        public const int error_Type_CreateError = 10;
        /// <summary>
        /// Json数据解析错误返回值
        /// </summary>
        public const int error_Type_JsonAnalysisEroor = 20;
    }
}
