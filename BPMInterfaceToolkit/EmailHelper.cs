using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Configuration;
using BPMInterfaceModel;

namespace BPMInterfaceToolkit
{
    public class EmailHelper
    {
        //static string MailFrom = "AttendanceAdmin@shry.net";
        //static string Password = "91338720";
        //static string smtp = "smtp.ee-post.com";
        //static int port = 25;

        static string MailFrom = ConfigurationManager.AppSettings["EmailAccount"].Trim();
        static string Password = Des.Decrypt(ConfigurationManager.AppSettings["EmailPWD"].Trim(), "91338720");
        static string smtp = ConfigurationManager.AppSettings["SMTPServer"].Trim();
        static int port = Convert.ToInt32(ConfigurationManager.AppSettings["SMTPPort"].Trim());

        /// <summary>
        /// 发送邮件(单人)
        /// </summary>
        /// <param name="EmailAddress">邮件地址</param>
        /// <param name="Subject">主题</param>
        /// <param name="Body">内容</param>
        public static bool SendMail(string EmailAddress, string Subject, string Body)
        {
            bool _b = false;
            string[] emails = EmailAddress.Split(';');
            foreach (string email in emails)
            {
                //邮件信息
                MailAddress ma = new MailAddress(MailFrom, "考勤辅助系统");
                MailMessage mail = new MailMessage();
                mail.From = ma;
                

                //验证电子邮件格式是否正确
                Regex regExp = new Regex("^\\w+([-+.]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$");
                Match m = regExp.Match(email);
                if (m.Success)
                {
                    mail.To.Add(email);
                    //邮件Title
                    mail.Subject = Subject.Trim();
                    //邮件主体
                    mail.Body = Body.Trim();
                    mail.IsBodyHtml = true;

                    SmtpClient sc = new SmtpClient(smtp.Trim(), port);
                    sc.Credentials = new NetworkCredential(MailFrom.Trim(), Password.Trim());

                    try
                    {
                        sc.Send(mail);
                        _b = true;
                        Log.WriteLog(mail.To.ToString().Trim() + "发送成功！");
                        //File.AppendAllText("C:\\AutoSendMail.txt", string.Format("{0}邮件发送成功！\r\n", dr_mailaddress[0]["姓名"].ToString().Trim()));
                    }
                    catch (Exception ee)
                    {
                        Log.WriteLog("邮件发送失败，" + ee.ToString());
                        //File.AppendAllText("C:\\AutoSendMail.txt", "发送失败！" + ee.ToString());
                    }
                    finally
                    {
                        sc.Dispose();
                        mail.Dispose();
                        GC.Collect();
                    }

                }
                else
                {
                    Log.WriteLog("电子邮件格式不正确！");
                }
            }

            return _b;
        }

        /// <summary>
        /// 群发邮件
        /// </summary>
        /// <param name="emailto">收件人地址</param>
        /// <param name="emailcc">抄送地址</param>
        /// <param name="subject">主题</param>
        /// <param name="body">内容</param>
        /// <param name="displayname">显示信息</param>
        /// <returns></returns>
        public static bool SendMails(string emailto, string emailcc,string subject, string body, string displayname)
        {
            bool _b = false;
            //邮件信息
            MailAddress ma = new MailAddress(MailFrom, displayname);
            MailMessage mail = new MailMessage();
            mail.From = ma;

            emailto = CheckEmailAddress(emailto);
            emailcc = CheckEmailAddress(emailcc);

            if (!emailto.Trim().Equals("") && !emailcc.Trim().Equals(""))
            {
                foreach (string str in emailto.Trim().Split(';'))
                {
                    mail.To.Add(str);
                }
                foreach (string str_cc in emailcc.Trim().Split(';'))
                {
                    mail.CC.Add(str_cc);
                }
            }
            else if (emailto.Trim().Equals("") && !emailcc.Trim().Equals(""))
            {
                foreach (string str in emailcc.Trim().Split(';'))
                {
                    mail.To.Add(str);
                }
            }
            else if (!emailto.Trim().Equals("") && emailcc.Trim().Equals(""))
            {
                foreach (string str in emailto.Trim().Split(';'))
                {
                    mail.To.Add(str);
                }
            }
            else
            {
                Log.WriteLog("收件人、抄送人地址都为空");
                return false;
            }
            //邮件Title
            mail.Subject = subject.Trim();
            //邮件主体
            mail.Body = body.Trim();
            mail.IsBodyHtml = true;

            SmtpClient sc = new SmtpClient(smtp.Trim(), port);
            sc.Credentials = new NetworkCredential(MailFrom.Trim(), Password.Trim());

            try
            {
                sc.Send(mail);
                _b = true;
                Log.WriteLog(mail.To.ToString().Trim() + "发送成功！");
            }
            catch (Exception ee)
            {
                Log.WriteLog("邮件发送失败，" + ee.ToString());
            }
            finally
            {
                sc.Dispose();
                mail.Dispose();
                GC.Collect();
            }
            return _b;
        }


        private static string CheckEmailAddress(string emails)
        {
            string emailadd = "";
            string[] emailadds = emails.Trim().Split(';');
            foreach (string str_email in emailadds)
            {
                //验证电子邮件格式是否正确
                Regex regExp = new Regex("^\\w+([-+.]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$");
                Match m = regExp.Match(str_email);
                if (m.Success)
                {
                    emailadd += str_email + ";";
                }
                else
                {
                    Log.WriteLog(string.Format("{0}不是有效的email地址！", str_email));
                }
            }
            if (!emailadd.Trim().Equals(""))
            {
                emailadd = emailadd.Trim().Substring(0, emailadd.Length - 1);
            }
            return emailadd;
        }

        /// <summary>
        /// 生成邮件内容
        /// </summary>
        /// <param name="billtype">单据类型</param>
        /// <param name="errortype">错误类型</param>
        /// <param name="showmessage">需显示的内容</param>
        /// <param name="errordata">错误数据提示</param>
        /// <returns></returns>
        public static string CreateEmailBody(string billtype, int errortype, string showmessage, string errordata)
        {
            string body = "";
            switch (billtype)
            {
                case "用款申请单":
                    switch (errortype)
                    {
                        case GlobalInfoModel.error_Type_CreateError:
                            body = string.Format(@"<p>
	                                                <span style='font - size:16px;'>{0}：<span style='color:#E53333;font-size:24px;'>{1}</span>&nbsp; 生成支付单失败！</span>
                                                   </p>
                                                   <p>
                                                    <span style = 'font-size:16px;'> 失败原因： {2} </ span >
                                                   </p><br/>
                                                   <p>        
                                                    <span style = 'color:#337FE5;'> --------系统邮件，请勿回复-------- </span >
                                                   </p>", billtype.Trim(), showmessage.Trim(), errordata.Trim());
                            break;
                        case GlobalInfoModel.error_Type_JsonAnalysisEroor:
                            body = string.Format(@"<p>
	                                                <span style='font - size:16px;'>{0}生成支付单失败</span>
                                                   </p>
                                                   <p>
                                                    <span style = 'font-size:16px;'> 失败原因：{1}<br/>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;{2} </ span >
                                                   </p><br/>
                                                   <p>        
                                                    <span style = 'color:#337FE5;'> --------系统邮件，请勿回复-------- </span >
                                                   </p>", billtype.Trim(), showmessage.Trim(), errordata.Trim());
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }


            body = string.Format(@"<html><head></head><body>{0}</body></html>",body.Trim());
            return body;
        }
    }
}
