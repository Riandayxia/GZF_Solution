using Quest.Mobile.CommonSupport.Filter;
using System;
using System.Configuration;
using System.Web.Mvc;

namespace Quest.Mobile
{
    [AdminAuthorize]
    [Exception]
    public class BaseController: Controller
    {
        /// <summary>
        /// 邮件发送
        /// </summary>
        /// <param name="to"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="bcc"></param>
        /// <returns></returns>
        protected String SendEmail(String to, String subject, String body, String bcc = "")
        {
            if (String.IsNullOrEmpty(to)) return "没有收件人";
            if (String.IsNullOrEmpty(subject)) return "没有邮件标题";

            //String smtpServer = ConfigInfo.Get("smtp-server");
            //String smtpUser = ConfigInfo.Get("smtp-user");
            //String smtpPassword = ConfigInfo.Get("smtp-password");
            //String smtpFrom = ConfigInfo.Get("smtp-from");
            //String smtpDispaly = ConfigInfo.Get("smtp-display");
            try
            {
                //System.Net.Mail.SmtpClient client = new SmtpClient();
                //client.Host = smtpServer;
                //client.UseDefaultCredentials = false;
                //client.Credentials = new System.Net.NetworkCredential(smtpUser, smtpPassword);
                //client.DeliveryMethod = SmtpDeliveryMethod.Network;
                //System.Net.Mail.MailMessage message = new MailMessage();
                //message.From = new MailAddress(smtpFrom, smtpDispaly);
                //message.To.Add(to.Trim());
                //if (!String.IsNullOrEmpty(bcc)) message.Bcc.Add(bcc);
                ////if (!String.IsNullOrEmpty(cc)) message.CC.Add(cc);
                //message.Subject = subject;
                //message.Body = body;
                //message.BodyEncoding = System.Text.Encoding.UTF8;
                //message.IsBodyHtml = true;
                //client.Send(message);
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// 当前登录用户为超级管理员
        /// </summary>
        protected Boolean IsAdministrator
        {
            get
            {
                return this.HaveRole(ConfigurationManager.AppSettings["Admin"]);
            }
        }

        /// <summary>
        /// 拥有指定角色
        /// </summary>
        /// <param name="adminId">管理员Id</param>
        /// <returns>返回操作结果</returns>
        protected Boolean HaveRole(String roleCode)
        {
            return true; //CurrentUser.GetRoles().Exists(p => p.Code == roleCode); ;
        }
    }
}
