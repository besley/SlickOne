using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;

namespace SlickOne.WebUtility
{
    /// <summary>
    /// 发送邮件工具类
    /// </summary>
    public class EMailUtility
    {
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="title">邮件主题</param>
        /// <param name="content">邮件内容</param>
        /// <param name="receiveEmail">收件人</param>
        public static void SendEMail(string title, string content, string receiveEmail)
        {
            var sendEMailAccount = ConfigurationManager.AppSettings["SendEMailAccount"];
            var sendEMailPassword = ConfigurationManager.AppSettings["SendEMailPassword"];
            var sendEMailHost = ConfigurationManager.AppSettings["SendEMailHost"];

            //发件人邮箱账号
            string eMailUserName = sendEMailAccount;
            //发件人邮箱密码
            string eMailPassWord = sendEMailPassword;
            //邮箱SMTP主机
            string eMailHost = sendEMailHost;
            //端口
            int eMailPort = 25;
            //是否使用安全连接传输
            bool eMailEnableSSL = true;
            //发件人地址
            string eMailFromAddress = sendEMailAccount;

            SmtpClient smtp = new SmtpClient();
            smtp.Credentials = new System.Net.NetworkCredential(eMailUserName, eMailPassWord);
            smtp.Host = eMailHost;
            smtp.Port = eMailPort;
            smtp.EnableSsl = eMailEnableSSL;

            //邮件信息
            MailMessage mail = new MailMessage();
            mail.Subject = title;
            mail.Body = content;
            mail.From = new MailAddress(eMailFromAddress);
            mail.SubjectEncoding = System.Text.UTF8Encoding.UTF8;
            mail.BodyEncoding = System.Text.UTF8Encoding.UTF8;
            mail.Priority = MailPriority.High;
            mail.IsBodyHtml = true;
            mail.To.Add(receiveEmail);
            smtp.SendAsync(mail, null);//发送邮件
        }
    }
}
